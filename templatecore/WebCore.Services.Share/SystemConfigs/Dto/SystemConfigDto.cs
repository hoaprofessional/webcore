using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Entities;

namespace WebCore.Services.Share.SystemConfigs.Dto
{
    public class SystemConfigDto : IBaseDto<SystemConfig>
    {
        public string Key { get; set; }
        public string ValueString { get; set; }
        public decimal ValueNumber { get; set; }
    }
}
