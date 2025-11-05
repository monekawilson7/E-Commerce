using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Presistence.Context.Configurations;
internal class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(i=>i.product, o=>o.WithOwner());

        builder.Property(o => o.Price)
       .HasColumnType("decimal(18,2)");

    }
}
