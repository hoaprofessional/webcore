using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Entities;

namespace WebCore.Services.Share.AppMenus.Dto
{
    public class AppMenuDto : IBaseDto<AppMenu>
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string Permission { get; set; }
        public int ParentId { get; set; }
        public List<AppMenuDto> Childs { get; set; }
    }
}
