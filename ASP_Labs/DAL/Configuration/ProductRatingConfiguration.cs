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
                .WithMany(p => p.Ratings)
                .HasForeignKey(b => b.ProductId);

            builder
               .HasOne<ApplicationUser>()
               .WithMany(u => u.Ratings)
               .HasForeignKey(b => b.UserId);

            builder
                .Property(b => b.ProductId)
                .IsRequired();
            
            builder
                .Property(b => b.UserId)
                .IsRequired();
            builder
                .Property(b => b.Rating)
                .IsRequired();

            builder.HasIndex(b => b.Rating);
        }
    }
}
