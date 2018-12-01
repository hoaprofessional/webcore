namespace WebCore.Services.Share.Admins.Users
{
    using Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IUserService
    {
        Task<bool> Add(UserInfoInput addInput);
        Task<bool> UpdateInfo(UserInfoInput updateInput);
        Task<bool> Delete(EntityId<string> entityId);
        Task<bool> SetActiveAsync(EntityId<string> entityId, long recordStatus);
        PagingResultDto<UserDto> GetAllByPaging(UserFilterInput filterInput);
        WebCoreUser GetById(EntityId<string> entityId);
        UserInfoInput GetInputById(EntityId<string> entityId);
        UserResetPasswordInput GetResetPasswordInputById(EntityId<string> entityId);
        Task<string[]> GetAllClaimsAsync(EntityId<string> userId);
        Task<List<RoleDto>> GetAllRolesAsync(EntityId<string> userId);

        Task<bool> UpdatePermissionsAsync(AssignPermissionInput assignPermissionInput);
    }
}
