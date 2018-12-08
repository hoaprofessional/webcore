namespace WebCore.Services.Share.Languages
{
    using Dto;
    using System.Collections.Generic;

    public interface ILanguageProviderService
    {
        List<LanguageDetailDto> GetLanguagesByCode(string code);
        string GetlangByKey(string key);
        void UpdateLanguage(string code, string key, string value);
    }
}
