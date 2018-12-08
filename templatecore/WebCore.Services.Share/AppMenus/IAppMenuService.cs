using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Services.Share.AppMenus.Dto;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.AppMenus
{
    public interface IAppMenuService
    {
        List<AppMenuDto> GetAllMenuByPermission(List<string> permissions);
    }
}
