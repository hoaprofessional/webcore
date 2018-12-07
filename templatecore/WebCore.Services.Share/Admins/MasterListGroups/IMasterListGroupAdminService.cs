
namespace WebCore.Services.Share.Admins.MasterListGroups
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IMasterListGroupAdminService
    {
        PagingResultDto<MasterListGroupDto> GetAllByPaging(MasterListGroupFilterInput adminMenuFilterInput);
        //SelectList GetMasterListGroupsCombobox();
        MasterList GetById(EntityId<int> idModel);
        MasterListGroupInput GetInputById(EntityId<int> idModel);
        MasterListGroupInput Add(MasterListGroupInput inputModel);
        bool Update(MasterListGroupInput inputModel);
        bool Delete(EntityId<int> idModel);
        bool Restore(EntityId<int> idModel);
    }
}
