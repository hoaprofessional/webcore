
namespace WebCore.Services.Share.Admins.LanguageDetails
{
    using Dto;
    using WebCore.Entities;
    using WebCore.Utils.ModelHelper;

    public interface ILanguageDetailAdminService
    {
        PagingResultDto<LanguageDetailDto> GetAllByPaging(LanguageDetailFilterInput languageFilterInput);

        LanguageDetailInput Add(LanguageDetailInput input);
        bool Update(LanguageDetailInput input);
        bool Delete(EntityId<int> idModel);
        LanguageDetailInput GetInputById(EntityId<int> idModel);
        LanguageDetail GetById(EntityId<int> idModel);
    }
}
