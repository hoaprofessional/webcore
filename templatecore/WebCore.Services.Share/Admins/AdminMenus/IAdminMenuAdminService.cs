
namespace WebCore.Services.Share.Admins.AdminMenus
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IAdminMenuAdminService
    {
        PagingResultDto<AdminMenuDto> GetAllByPaging(AdminMenuFilterInput adminMenuFilterInput);
        SelectList GetAdminMenusCombobox();
        AdminMenu GetById(EntityId<int> idModel);
        AdminMenuInput GetInputById(EntityId<int> idModel);
        AdminMenuInput AddAdminMenu(AdminMenuInput inputModel);
        bool UpdateAdminmenu(AdminMenuInput inputModel);
        bool DeleteAdminMenu(EntityId<int> idModel);
        bool RestoreAdminMenu(EntityId<int> idModel);
    }
}
