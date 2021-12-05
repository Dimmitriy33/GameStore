using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(b => b.OrderId);

            builder
                .HasOne(t => t.Product)
                .WithMany(t => t.OrdersList)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

            builder
                .HasOne(t => t.User)
                .WithMany(t => t.OrdersList)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder
                .Property(b => b.Amount)
                .IsRequired();

            builder
                .Property(b => b.Status)
                .HasDefaultValue(OrderStatus.Unpaid);

            builder.HasIndex(b => b.Status);
            builder.HasIndex(b => b.OrderId);
            builder.HasIndex(b => b.ProductId);
            builder.HasIndex(b => b.UserId);
        }
    }
}
