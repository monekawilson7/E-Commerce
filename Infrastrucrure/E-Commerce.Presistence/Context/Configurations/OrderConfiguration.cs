using Order = E_Commerce.Domain.Entities.OrderEntities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Presistence.Context.Configurations;
internal class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(x=>x.Items)
               .WithOne()
               .HasForeignKey(x=>x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(o=>o.DeliveryMethod)
               .WithMany()
               .HasForeignKey(x=>x.DeliveryMethodId)
               .OnDelete(DeleteBehavior.SetNull);
        builder.OwnsOne(o=>o.Address, x=>x.WithOwner());

        builder.Property(o=>o.Subtotal)
               .HasColumnType("decimal(18,2)");

        builder.Property(o=>o.UserEmail)
               .HasColumnType("VarChar")
               .HasMaxLength(128);

        builder.HasIndex(o => o.UserEmail);

        builder.Property(o=> o.Status)
               .HasConversion<string>();

        builder.Property(o => o.PaymentIntentId)
               .HasColumnType("VarChar")
               .HasMaxLength(128);
    }
}
