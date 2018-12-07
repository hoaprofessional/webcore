using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Data;
using WebCore.Utils.Config;

namespace WebCore.EntityFramework.Seeds
{
    public class MasterListSeeder : BaseSeeder
    {
        public override void InitDb(WebCoreDbContext context)
        {
            Microsoft.EntityFrameworkCore.DbSet<MasterList> masterList = context.MasterLists;
            if (masterList.Count() == 0)
            {
                masterList.Add(new MasterList()
                {
                    Group = ConstantConfig.MasterListMasterGroup,
                    Value = ConstantConfig.MasterListMasterGroup,
                    RecordStatus = ConstantConfig.RecordStatusConfig.Active,
                    OrderNo = 0
                });
                context.SaveChanges();
            }
        }
    }
}
