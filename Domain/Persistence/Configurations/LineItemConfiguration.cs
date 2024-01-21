using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(lineItem => lineItem.Price);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);
            builder.OwnsOne(x => x.Price);
        }
    }
}
