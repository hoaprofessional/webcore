
namespace WebCore.Services.Share.Admins.MasterLists
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IMasterListAdminService
    {
        SortingAndPagingResultDto<MasterListDto> GetAllByPaging(MasterListFilterInput masterListFilterInput);
        MasterList GetById(EntityId<int> idModel);
        MasterListInput GetInputById(EntityId<int> idModel);
        MasterListInput Add(MasterListInput inputModel);
        bool Update(MasterListInput inputModel);
        bool Delete(EntityId<int> idModel);
        bool Restore(EntityId<int> idModel);
    }
}
