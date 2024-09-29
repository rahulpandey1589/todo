using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Todo.Persistence.Domain;

namespace Todo.Persistence.Configurations
{
    public class ProductionDataConfiguration : IEntityTypeConfiguration<ProductionData>
    {
        public void Configure(
            EntityTypeBuilder<ProductionData> builder)
        {
            builder.ToTable("ProductionData", "Production");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CommodityName).HasMaxLength(100);
        }
    }
}
