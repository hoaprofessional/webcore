using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCore.EntityFramework.Data;
using WebCore.Utils.Config;

namespace WebCore.EntityFramework.Seeds
{
    public class SystemConfigSeeder : BaseSeeder
    {
        public override void InitDb(WebCoreDbContext context)
        {
            var pageDefaultNumber = context.SystemConfigs.Where(x => x.Key == "PageDefaultNumber").FirstOrDefault();
            if(pageDefaultNumber==null)
            {
                context.SystemConfigs.Add(new Entities.SystemConfig()
                {
                    Key = ConstantConfig.SystemConfigName.PageDefaultNumber,
                    ValueNumber = 10,
                    ValueString = ""
                });
                context.SaveChanges();
            }
        }
    }
}
