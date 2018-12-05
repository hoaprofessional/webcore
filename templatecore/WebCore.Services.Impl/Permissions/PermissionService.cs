using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebCore.Entities;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.Permissions;
using WebCore.Services.Share.Permissions.Dto;
using WebCore.Utils.Commons;
using WebCore.Utils.Config;
using WebCore.Utils.ModelHelper;
using WebCore.Utils.TreeViewHelper;

namespace WebCore.Services.Impl.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<WebCoreRole, string> roleRepository;
        private readonly RoleManager<WebCoreRole> roleManager;
        private readonly ILanguageProviderService languageProviderService;

        public PermissionService(IRepository<WebCoreRole, string> roleRepository, ILanguageProviderService languageProviderService, RoleManager<WebCoreRole> roleManager)
        {
            this.roleRepository = roleRepository;
            this.roleManager = roleManager;
            this.languageProviderService = languageProviderService;
        }

        public HashSet<string> GetAllPermissions()
        {
            IEnumerable<FieldInfo> allPermissionStaticMembers = typeof(ConstantConfig.Claims)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .Where(f => f.FieldType == typeof(string));

            return allPermissionStaticMembers.Select(x => x.GetValue(null).ToString()).ToHashSet();
        }

        public async Task<SelectList> GetPermissionCombobox()
        {
            List<PermissionDto> permissions = await GetAllPermissionDto();
            List<ComboboxResult<string, string>> allPermissions = permissions.Select(x => new ComboboxResult<string, string>()
            {
                Value = x.Key.ToString(),
                Display = languageProviderService.GetlangByKey(x.Name)
            }).ToList();
            return allPermissions.ToSelectList();
        }

        public async Task<PermissionDto> GetPermissionTreeViewAsync(string[] checkedPermissions)
        {
            List<PermissionDto> permissions = await GetAllPermissionDto();
            foreach (PermissionDto permission in permissions)
            {
                permission.Checked = checkedPermissions.Contains(permission.Key);
            }
            PermissionDto rootNode = permissions.ToTreeView("rootkey");
            return rootNode;
        }

        private async Task<List<PermissionDto>> GetAllPermissionDto()
        {
            IQueryable<WebCoreRole> roles = roleRepository.GetAll();


            IEnumerable<FieldInfo> allPermissionStaticMembers = typeof(ConstantConfig.Claims)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .Where(f => f.FieldType == typeof(string));

            List<PermissionDto> permissions = allPermissionStaticMembers.Select(x => new PermissionDto()
            {
                Key = x.GetValue(null),
                Name = x.Name
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

            foreach (WebCoreRole role in roles)
            {
                string[] permissionsOfRole = await GetAllPermissionsAsync(role);

                foreach (PermissionDto permission in permissions)
                {
                    if (permissionsOfRole.Contains(permission.Key))
                    {
                        permission.Roles.Add(role.Name);
                    }
                }
            }
            return permissions;
        }

        private async Task<string[]> GetAllPermissionsAsync(WebCoreRole role)
        {
            return (await roleManager.GetClaimsAsync(role)).Select(x => x.Value).ToArray();
        }
    }
}
