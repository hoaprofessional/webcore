namespace WebCore.Services.Share.Permissions
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPermissionService
    {
        Task<PermissionDto> GetPermissionTreeViewAsync(string[] checkedPermissions);
        HashSet<string> GetAllPermissions();
        Task<SelectList> GetPermissionCombobox();
    }
}
