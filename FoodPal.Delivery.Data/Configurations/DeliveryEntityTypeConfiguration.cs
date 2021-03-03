using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodPal.Delivery.Data.Configurations
{
    public class DeliveryEntityTypeConfiguration : IEntityTypeConfiguration<Models.Delivery>
    {
        public void Configure(EntityTypeBuilder<Models.Delivery> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Info)
                .HasMaxLength(1000);

            builder
                .HasOne(d => d.User)
                .WithMany(u => u.Deliveries)
                .HasForeignKey(d => d.UserId);
        }
    }
}
