using E_Commerce.Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Presistence.Context.Configurations;
internal class DeliveryMethodConfiguration
    : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(o => o.Price)
       .HasColumnType("decimal(18,2)");

        builder.Property(o => o.ShortName)
               .HasColumnType("VarChar")
               .HasMaxLength(128);

        builder.Property(o => o.DeliveryTime)
       .HasColumnType("VarChar")
       .HasMaxLength(128);

        builder.Property(o => o.Description)
       .HasColumnType("VarChar")
       .HasMaxLength(128);
    }
}
