using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.DAL.Entities;

namespace WebApp.DAL.Configuration
{
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {

            builder.HasKey(b => new { b.ProductId, b.UserId });

            builder
                .HasOne<Product>()
                .WithMany(t => t.Ratings)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

            builder
               .HasOne<ApplicationUser>()
               .WithMany(u => u.Ratings)
               .HasForeignKey(b => b.UserId)
                .IsRequired();

            builder
                .Property(b => b.Rating)
                .IsRequired();

            builder.HasIndex(b => b.Rating);
        }
    }
}
