
namespace WebCore.Services.Share.Admins.Featureds
{
    using Dto;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface IFeaturedAdminService
    {
        SortingAndPagingResultDto<FeaturedDto> GetAllByPaging(FeaturedFilterInput masterListFilterInput);
        Featured GetById(EntityId<int> idModel);
        FeaturedInput GetInputById(EntityId<int> idModel);
        FeaturedInput Add(FeaturedInput inputModel);
        bool Update(FeaturedInput inputModel);
        bool Delete(EntityId<int> idModel);
        bool Restore(EntityId<int> idModel);
    }
}
