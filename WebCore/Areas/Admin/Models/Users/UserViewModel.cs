using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Areas.Admin.Models.Users
{
    public class UserViewModel : AdminBaseViewModel
    {
        public PagingResultDto<UserDto> PagingResult { get; set; }
        public UserFilterInput FilterInput { get; set; }
    }
}
