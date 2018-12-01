using System;
using System.Collections.Generic;
using System.Text;
using WebCore.EntityFramework.Data;

namespace WebCore.EntityFramework.Seeds
{
    public abstract class BaseSeeder
    {
        public abstract void InitDb(WebCoreDbContext context);
    }
}
