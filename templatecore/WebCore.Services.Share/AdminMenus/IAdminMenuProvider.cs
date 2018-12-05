using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.AdminMenus
{
    using Dto;
    public interface IAdminMenuProvider
    {
        AdminMenuTreeViewDto GetAdminMenuTreeView(string[] permissions);
    }
}
