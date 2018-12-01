using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Permissions;
using WebCore.Services.Share.Permissions.Dto;
using WebCore.Utils.Config;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.Services.Impl.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<WebCoreRole, string> roleRepository;
        private readonly RoleManager<WebCoreRole> roleManager;

        public PermissionService(IRepository<WebCoreRole, string> roleRepository, RoleManager<WebCoreRole> roleManager)
        {
            this.roleRepository = roleRepository;
            this.roleManager = roleManager;
        }

        public HashSet<string> GetAllPermissions()
        {
            IEnumerable<FieldInfo> allPermissionStaticMembers = typeof(ConstantConfig.Claims)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .Where(f => f.FieldType == typeof(string));

            return allPermissionStaticMembers.Select(x => x.GetValue(null).ToString()).ToHashSet();
        }

        public async Task<PermissionDto> GetPermissionTreeViewAsync(string[] checkedPermissions)
        {
            IQueryable<WebCoreRole> roles = roleRepository.GetAll();


            IEnumerable<FieldInfo> allPermissionStaticMembers = typeof(ConstantConfig.Claims)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .Where(f => f.FieldType == typeof(string));

            List<PermissionDto> permissions = allPermissionStaticMembers.Select(x => new PermissionDto()
            {
                Key = x.GetValue(null),
                Name = x.Name,
                Checked = checkedPermissions.Contains(x.GetValue(null))
            }).ToList();
            
            foreach (PermissionDto permission in permissions)
            {
                string permissionString = (string)permission.Key;
                if (permissionString.IndexOf(".") > 0)
                {
                    permission.ParentKey = permissionString.Substring(0, permissionString.LastIndexOf("."));
                }
                else
                {
                    permission.ParentKey = "-";
                }

                permission.Roles = new List<string>();
            }

            foreach (var role in roles)
            {
                var permissionsOfRole = await GetAllPermissionsAsync(role);

                foreach (PermissionDto permission in permissions)
                {
                    if(permissionsOfRole.Contains(permission.Key))
                    {
                        permission.Roles.Add(role.Name);
                    }
                }
            }

            PermissionDto rootNode = permissions.ToTreeView("rootkey");
            return rootNode;
        }
        private async Task<string[]> GetAllPermissionsAsync(WebCoreRole role)
        {
            return (await roleManager.GetClaimsAsync(role)).Select(x => x.Value).ToArray();
        }
    }
}
