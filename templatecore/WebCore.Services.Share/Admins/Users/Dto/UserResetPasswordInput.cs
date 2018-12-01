using System.ComponentModel.DataAnnotations;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Users.Dto
{
    /// <summary>
    /// Model WebCoreUser
    /// </summary>
    public class UserResetPasswordInput : UpdateTokenModel<string>
    {
        [Required(ErrorMessage = "LBL_USER_PASSWORD_REQUIRED")]
        [StringLength(100, ErrorMessage = "LBL_USER_PASSWORD_LENGTH_INVALID", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "LBL_USER_CONFIRM_PASSWORD_NOTMATCH")]
        public string ConfirmPassword { get; set; }
    }
}
