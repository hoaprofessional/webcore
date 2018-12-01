using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.Users;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Permissions;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<WebCoreUser> userManager;
        private readonly IRepository<WebCoreUser, string> userRepository;
        private readonly IRepository<WebCoreRole, string> roleRepository;
        private readonly IMapper mapper;
        private readonly IPermissionService permissionService;



        public UserService(IServiceProvider serviceProvider, UserManager<WebCoreUser> userManager, IRepository<WebCoreUser, string> userRepository,
            IPermissionService permissionService,
            IRepository<WebCoreRole, string> roleRepository, IMapper mapper)
            : base(serviceProvider)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionService = permissionService;
            this.mapper = mapper;
        }

        public async Task<bool> Add(UserInfoInput addInput)
        {
            WebCoreUser entity = mapper.Map<WebCoreUser>(addInput);

            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = entity.CreatedBy;
            entity.ModifiedDate = entity.CreatedDate;
            entity.UpdateToken = Guid.NewGuid();

            IdentityResult result = await userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(EntityId<string> entityId)
        {
            WebCoreUser entity = userRepository.GetById(entityId.Id);

            entity.RecordStatus = ConstantConfig.UserRecordStatus.Deleted;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();

            IdentityResult result = await userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> SetActiveAsync(EntityId<string> entityId, long recordStatus)
        {
            WebCoreUser entity = userRepository.GetById(entityId.Id);

            entity.RecordStatus = recordStatus;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();

            IdentityResult result = await userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public PagingResultDto<UserDto> GetAllByPaging(UserFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<WebCoreUser> userQuery = userRepository.GetAll();

            userQuery = userQuery.Filter(filterInput);

            PagingResultDto<UserDto> userResult = userQuery
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .PagedQuery(filterInput);

            return userResult;
        }


        public async Task<bool> UpdateInfo(UserInfoInput updateInput)
        {
            WebCoreUser entity = userRepository.GetById(updateInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(updateInput, entity);

            entity.RecordStatus = ConstantConfig.UserRecordStatus.Active;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();
            IdentityResult result = await userManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public UserInfoInput GetInputById(EntityId<string> entityId)
        {
            WebCoreUser entity = userRepository.GetById(entityId.Id);

            UserInfoInput updateInput = new UserInfoInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<UserInfoInput>(entity);

            return updateInput;
        }

        public UserResetPasswordInput GetResetPasswordInputById(EntityId<string> entityId)
        {
            WebCoreUser entity = userRepository.GetById(entityId.Id);

            UserResetPasswordInput updateInput = new UserResetPasswordInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<UserResetPasswordInput>(entity);

            return updateInput;
        }


        public WebCoreUser GetById(EntityId<string> entityId)
        {
            return userRepository.GetById(entityId.Id);
        }

        public async Task<string[]> GetAllClaimsAsync(EntityId<string> userId)
        {
            WebCoreUser user = await userManager.FindByIdAsync(userId.Id);
            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            return claims.Select(x => x.Value).ToArray();
        }

        public async Task<List<RoleDto>> GetAllRolesAsync(EntityId<string> userId)
        {
            List<RoleDto> roles = roleRepository.GetAll().Select(x => new RoleDto()
            {
                RoleName = x.Name,
                IsChecked = false
            }).ToList();

            WebCoreUser user = userRepository.GetById(userId.Id);
            IList<string> allRolesOfUser = await userManager.GetRolesAsync(user);

            foreach (RoleDto role in roles)
            {
                if (allRolesOfUser.Any(x => x == role.RoleName))
                {
                    role.IsChecked = true;
                }
            }

            return roles;
        }

        public async Task<bool> UpdatePermissionsAsync(AssignPermissionInput assignPermissionInput)
        {
            WebCoreUser user = userRepository.GetById(assignPermissionInput.UserId);
            string[] viewRoles = assignPermissionInput.Roles;

            EntityId<string> userIdModel = new EntityId<string>() { Id = assignPermissionInput.UserId };

            List<RoleDto> allRoles = await GetAllRolesAsync(userIdModel);

            string[] allUserRoles = allRoles.Where(x => x.IsChecked).Select(x => x.RoleName).ToArray();


            HashSet<string> allClaims = permissionService.GetAllPermissions();
            IList<Claim> allClaimsOfUser = await userManager.GetClaimsAsync(user);
            string[] viewClaims = assignPermissionInput.Permissions;

            if (viewRoles.Any(vr => allRoles.Count(r => r.RoleName == vr) == 0))
            {
                return false;
            }

            if (viewClaims.Any(vc => allClaims.Count(ac => ac == vc) == 0))
            {
                return false;
            }

            // roles need delete
            List<string> rolesNeedDelete = allUserRoles
                                .Where(ur => viewRoles.Count(vr => vr == ur) == 0)
                                .ToList();

            // roles need add
            List<string> rolesNeedAdd = viewRoles
                                .Where(vr => allUserRoles.Count(ur => ur == vr) == 0)
                                .ToList();

            foreach (string roleName in rolesNeedDelete)
            {
                await userManager.RemoveFromRoleAsync(user, roleName);
            }

            foreach (string roleName in rolesNeedAdd)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }

            

            // claims need delete
            IList<Claim> claimsNeedDelete = allClaimsOfUser
                                            .Where(uc => viewClaims.Count(vc => vc == uc.Value) == 0)
                                            .ToList();

            // claims need add
            List<string> claimsNeedAdd = viewClaims
                                            .Where(vc => allClaimsOfUser.Count(uc => vc == uc.Value) == 0)
                                            .ToList();


            foreach (string claim in claimsNeedAdd)
            {
                await userManager.AddClaimAsync(user, new Claim(ConstantConfig.ClaimType.Permission, claim));
            }

            foreach (var claim in claimsNeedDelete)
            {
                await userManager.RemoveClaimAsync(user, claim);
            }

            return true;
        }
    }
}
