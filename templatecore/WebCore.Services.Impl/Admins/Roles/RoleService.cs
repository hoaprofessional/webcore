using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCore.Entities;
using WebCore.EntityFramework.Helper;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Admins.Roles;
using WebCore.Services.Share.Admins.Roles.Dto;
using WebCore.Utils.CollectionHelper;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Impl.Admins.Roles
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<WebCoreRole> roleManager;
        private readonly IRepository<WebCoreRole, string> roleRepository;
        private readonly IRepository<WebCoreRoleClaim, int> roleClaimRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IServiceProvider serviceProvider, 
            RoleManager<WebCoreRole> roleManager, 
            IRepository<WebCoreRole, string> roleRepository,
            IUnitOfWork unitOfWork,
            IRepository<WebCoreRoleClaim, int> roleClaimRepository, IMapper mapper)
            : base(serviceProvider)
        {
            this.roleManager = roleManager;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
            this.roleClaimRepository = roleClaimRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(RoleInput addInput)
        {
            WebCoreRole entity = mapper.Map<WebCoreRole>(addInput);

            entity.CreatedBy = GetCurrentUserLogin();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = entity.CreatedBy;
            entity.ModifiedDate = entity.CreatedDate;
            entity.UpdateToken = Guid.NewGuid();

            IdentityResult result = await roleManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Active(EntityId<string> entityId)
        {
            WebCoreRole entity = roleRepository.GetById(entityId.Id);

            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();
            IdentityResult result = await roleManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public async Task<bool> Delete(EntityId<string> entityId)
        {
            WebCoreRole entity = roleRepository.GetById(entityId.Id);

            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Deleted;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();

            IdentityResult result = await roleManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public PagingResultDto<RoleDto> GetAllByPaging(RoleFilterInput filterInput)
        {
            SetDefaultPageSize(filterInput);

            IQueryable<WebCoreRole> roleQuery = roleRepository.GetAll();

            roleQuery = roleQuery.Filter(filterInput);

            PagingResultDto<RoleDto> roleResult = roleQuery
                .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                .PagedQuery(filterInput);

            return roleResult;
        }


        public async Task<bool> UpdateInfoAsync(RoleInput updateInput)
        {
            WebCoreRole entity = roleRepository.GetById(updateInput.Id);

            if (entity == null)
            {
                return false;
            }

            mapper.Map(updateInput, entity);

            entity.RecordStatus = ConstantConfig.RecordStatusConfig.Active;

            entity.ModifiedBy = GetCurrentUserLogin();
            entity.ModifiedDate = DateTime.Now;
            entity.UpdateToken = Guid.NewGuid();
            IdentityResult result = await roleManager.UpdateAsync(entity);

            return result.Succeeded;
        }

        public RoleInput GetInputById(EntityId<string> entityId)
        {
            WebCoreRole entity = roleRepository.GetById(entityId.Id);

            RoleInput updateInput = new RoleInput();

            if (entity == null)
            {
                return null;
            }

            updateInput = mapper.Map<RoleInput>(entity);

            return updateInput;
        }

        public WebCoreRole GetById(EntityId<string> entityId)
        {
            return roleRepository.GetById(entityId.Id);
        }

        public async Task<string[]> GetAllClaimsAsync(EntityId<string> roleId)
        {
            WebCoreRole role = await roleManager.FindByIdAsync(roleId.Id);
            IList<Claim> claims = await roleManager.GetClaimsAsync(role);
            return claims.Select(x => x.Value).ToArray();
        }

        public async Task UpdateClaimsAsync(EntityId<string> roleId, List<string> permissions)
        {
            try
            {
                WebCoreRole role = await roleManager.FindByIdAsync(roleId.Id);
                IList<Claim> databaseClaims = await roleManager.GetClaimsAsync(role);

                // Quyền cần xóa : những quyền trong database không nằm trong danh sách quyền từ client gửi lên
                IList<Claim> deleteClaims = databaseClaims.Where(x => !permissions.Contains(x.Value)).ToList();

                //Quyền cần thêm : những quyền ở client mà không có trong danh sách quyền của database
                string[] addClaims = permissions.Where(clientClaim => !databaseClaims.Any(dbClaim => dbClaim.Value == clientClaim)).ToArray();

                //Thực hiện xóa quyền
                foreach (Claim claim in deleteClaims)
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }

                //Thực hiện thêm quyền
                foreach (string claim in addClaims)
                {
                    await roleManager.AddClaimAsync(role, new Claim(ConstantConfig.ClaimType.Permission, claim));
                }
            }
            catch(Exception e)
            {
                throw;
            }
            
        }
    }
}
