namespace WebCore.Services.Share.Permissions
{
    using Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPermissionService
    {
        Task<PermissionDto> GetPermissionTreeViewAsync(string[] checkedPermissions);
        HashSet<string> GetAllPermissions();
    }
}
