using System;
using System.ComponentModel.DataAnnotations;
using WebCore.Services.Share.ValidationHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.LanguageDetails.Dto
{
    public class LanguageDetailInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_LANGUAGE_DETAIL_LANGUAGE_VALUE_REQUIRED")]
        public string LanguageValue { get; set; }
    }
}
