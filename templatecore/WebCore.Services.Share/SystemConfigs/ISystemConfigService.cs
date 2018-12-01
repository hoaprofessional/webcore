using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.SystemConfigs
{
    public interface ISystemConfigService
    {
        string GetValueString(string key);
        decimal GetValueNumber(string key);
    }
}
