using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Users.Dto
{
    /// <summary>
    /// Model WebCoreUser
    /// </summary>
    public class UserFilterInput : IPagingFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        [Filter(FilterComparison.Equal)]
        public string UserName { get; set; }
        [Filter(FilterComparison.Contains)]
        public string FirstName { get; set; }
        [Filter(FilterComparison.Contains)]
        public string Carrer { get; set; }
    }
}
