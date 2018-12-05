
namespace WebCore.Services.Share.Admins.MasterLists
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IMasterListAdminService
    {
        SelectList GetMasterListGroupCombobox();
        PagingResultDto<MasterListDto> GetAllByPaging(MasterListFilterInput masterListFilterInput);
        MasterList GetById(EntityId<int> idModel);
        MasterListInput GetInputById(EntityId<int> idModel);
        MasterListInput AddMasterList(MasterListInput inputModel);
        bool UpdateAdminmenu(MasterListInput inputModel);
        bool DeleteMasterList(EntityId<int> idModel);
        bool RestoreMasterList(EntityId<int> idModel);
    }
}
