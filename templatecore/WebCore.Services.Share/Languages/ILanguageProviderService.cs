namespace WebCore.Services.Share.Languages
{
    using Dto;
    using WebCore.Utils.ModelHelper;

    public interface ILanguageProviderService
    {
        ListResult<LanguageDetailDto> GetLanguagesByCode(string code);
        string GetlangByKey(string key);
        void UpdateLanguage(string code, string key, string value);
    }
}
