using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);
            
            builder.Property(o => o.OrderId)
                .HasConversion(orderItemId => orderItemId.Id, dbId => OrderId.Of(dbId));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.Price).IsRequired();
        }
    }
}
