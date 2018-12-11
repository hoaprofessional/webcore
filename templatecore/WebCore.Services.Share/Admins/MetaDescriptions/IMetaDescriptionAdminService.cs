
namespace WebCore.Services.Share.Admins.MetaDescriptions
{
    using Dto;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IMetaDescriptionAdminService
    {
        SortingAndPagingResultDto<MetaDescriptionDto> GetAllByPaging(MetaDescriptionFilterInput masterListFilterInput);
        MetaDescription GetById(EntityId<int> idModel);
        MetaDescriptionInput GetInputById(EntityId<int> idModel);
        MetaDescriptionInput Add(MetaDescriptionInput inputModel);
        bool Update(MetaDescriptionInput inputModel);
        bool Delete(EntityId<int> idModel);
        bool Restore(EntityId<int> idModel);
    }
}
