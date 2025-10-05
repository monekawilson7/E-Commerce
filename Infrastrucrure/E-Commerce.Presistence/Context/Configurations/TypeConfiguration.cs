using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Presistence.Context.Configurations;
internal class TypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(p=>p.Name)
            .HasColumnType("VarChar")
            .HasMaxLength(256);
    }
}
