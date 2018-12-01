
namespace WebCore.Services.Share.Admins.Languages
{
    using Dto;
    using System.Collections.Generic;
    using WebCore.Utils.ModelHelper;

    public interface ILanguageAdminService
    {
        PagingResultDto<LanguageDto> GetAllByPaging(LanguageFilterInput languageFilterInput);
        List<LanguageDto> GetAllLanguages();
    }
}
