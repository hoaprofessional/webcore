using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCore.Entities;

namespace WebCore.EntityFramework.Configurations
{
    public class MasterListConfiguration : IEntityTypeConfiguration<MasterList>
    {
        public void Configure(EntityTypeBuilder<MasterList> builder)
        {
            builder.HasIndex(x => x.Group);
        }
    }
}
