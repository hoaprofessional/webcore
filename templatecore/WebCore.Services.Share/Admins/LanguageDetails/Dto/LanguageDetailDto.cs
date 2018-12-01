using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.LanguageDetails.Dto
{
    public class LanguageDetailDto : EntityId<int>
    {
        public string LanguageCode { get; set; }
        public string LanguageKey { get; set; }
        public string LanguageValue { get; set; }
    }
}
