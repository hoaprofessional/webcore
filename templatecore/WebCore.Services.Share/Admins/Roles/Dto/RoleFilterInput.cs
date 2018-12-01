using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Roles.Dto
{
    /// <summary>
    /// Model WebCoreRole
    /// </summary>
    public class RoleFilterInput : IPagingFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        [Filter(FilterComparison.Contains)]
        public string Name { get; set; }
    }
}
