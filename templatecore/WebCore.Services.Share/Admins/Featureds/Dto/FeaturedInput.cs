using System;
using System.ComponentModel.DataAnnotations;
using WebCore.Utils.Attributes.Validations;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Featureds.Dto
{
    // Featured
    public class FeaturedInput : UpdateTokenModel<int>
    {
        [Required(ErrorMessage = "LBL_FEATURED_TEXT_REQUIRED")]
        public string Text { get; set; }

        public string NoValidateText { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_BETWEEN10TO20CHARACTER_REQUIRED")]
        [MaxLength(20, ErrorMessage = "LBL_FEATURED_BETWEEN10TO20CHARACTER_MAXLENGTH")]
        [MinLength(10, ErrorMessage = "LBL_FEATURED_BETWEEN10TO20CHARACTER_MINLENGTH")]
        public string Between10To20Character { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_NUMBER_REQUIRED")]
        public int? Number { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_BEETWEEN10TO20_REQUIRED")]
        [NumberBetween(NumberMinValue = 10, NumberMaxValue = 20, ErrorMessage = "LBL_FEATURED_BEETWEEN10TO20_BETWEEN")]
        public int? Beetween10To20 { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_DATE_REQUIRED")]
        [DataType(DataType.Date,ErrorMessage = "LBL_FEATURED_Date_INVALIDFORMAT")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_FULLDATEANDTIME_REQUIRED")]
        [DataType(DataType.Date, ErrorMessage = "LBL_FEATURED_Date_INVALIDFORMAT")]
        public DateTime? FullDateAndTime { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_TIME_REQUIRED")]
        [DataType(DataType.Time, ErrorMessage = "LBL_FEATURED_Time_INVALIDFORMAT")]
        public DateTime? Time { get; set; }

        [EmailAddress(ErrorMessage ="LBL_FEATURED_EMAIL_INVALIDFORMAT")]
        public string Email { get; set; }

        [Phone(ErrorMessage ="LBL_FEATURED_PHONENUMBER_INVALIDFORMAT")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_MONEY_REQUIRED")]
        [DataType(DataType.Currency)]
        [MoneyFormat(ErrorMessage = "LBL_FEATURED_MONEY_INVALIDFORMAT")]
        public decimal? Money { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_COMBOBOX_REQUIRED")]
        public string Combobox { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_MULTISELECT_REQUIRED")]
        public string MultiSelect { get; set; }

        [DateLessThanOrEqualCompareWith(PropertyName = nameof(ToDate), ErrorMessage = "LBL_FEATURED_FROMDATE_LESSTHAN")]
        [Required(ErrorMessage = "LBL_FEATURED_FROMDATE_REQUIRED")]
        public DateTime? FromDate { get; set; }

        [DateMoreThanOrEqualCompareWith(PropertyName = nameof(FromDate), ErrorMessage = "LBL_FEATURED_FROMDATE_MORETHAN")]
        [Required(ErrorMessage = "LBL_FEATURED_TODATE_REQUIRED")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_LESSTHANTODAY_REQUIRED")]
        [LessThanToday(ErrorMessage = "LESSTHANTODAY_INVALID")]
        public DateTime? LessThanToday { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_GREATERTHANTODAY_REQUIRED")]
        [GreaterThanToday(ErrorMessage = "GREATERTHANTODAY_INVALID")]
        public DateTime? GreaterThanToday { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_TEXTAREA_REQUIRED")]
        public string TextArea { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_TEXTEDITOR_REQUIRED")]
        public string TextEditor { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_IMAGE_REQUIRED")]
        public string Image { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_MULTIIMAGE_REQUIRED")]
        public string MultiImage { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_FILE_REQUIRED")]
        public string File { get; set; }

        [Required(ErrorMessage = "LBL_FEATURED_MULTIFILE_REQUIRED")]
        public string MultiFile { get; set; }
    }
}
