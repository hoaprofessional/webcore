namespace WebCore.Services.Share.Admins.Roles
{
    using Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IRoleService
    {
        Task<bool> Add(RoleInput addInput);
        Task<bool> UpdateInfoAsync(RoleInput updateInput);
        Task<bool> Delete(EntityId<string> entityId);
        Task<bool> Active(EntityId<string> entityId);
        PagingResultDto<RoleDto> GetAllByPaging(RoleFilterInput filterInput);
        WebCoreRole GetById(EntityId<string> entityId);
        RoleInput GetInputById(EntityId<string> entityId);
        Task<string[]> GetAllClaimsAsync(EntityId<string> idModel);
        Task UpdateClaimsAsync(EntityId<string> roleId, List<string> permissions);
    }
}
