using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("ProductId");
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Platform).IsRequired();
            builder.HasIndex(b => b.Name);
            builder.HasIndex(b => b.Platform);
            builder.HasIndex(b => b.TotalRating);
            builder.HasIndex(b => b.DateCreated);
            builder.Property("TotalRating").HasColumnType("decimal(4, 2)");
            builder.Property("Platform").HasColumnType("int");
            builder.Property(b => b.DateCreated).HasDefaultValueSql("getdate()");

            builder.HasData(
                new Product { Id = Guid.NewGuid(), Name = "FIFA 2020", Platform = Platforms.Playstation, TotalRating = 7.32M },
                new Product { Id = Guid.NewGuid(), Name = "God of War", Platform = Platforms.Playstation, TotalRating = 8.31M },
                new Product { Id = Guid.NewGuid(), Name = "Bloodborne", Platform = Platforms.Playstation, TotalRating = 6.81M },
                new Product { Id = Guid.NewGuid(), Name = "Among Us", Platform = Platforms.Mobile, TotalRating = 8.32M },
                new Product { Id = Guid.NewGuid(), Name = "Brawl Stars", Platform = Platforms.Mobile, TotalRating = 10M },
                new Product { Id = Guid.NewGuid(), Name = "Fortnite", Platform = Platforms.Xbox, TotalRating = 8.88M },
                new Product { Id = Guid.NewGuid(), Name = "Minecraft", Platform = Platforms.Xbox, TotalRating = 9.51M },
                new Product { Id = Guid.NewGuid(), Name = "Forza Horizon 4", Platform = Platforms.Xbox, TotalRating = 9.49M },
                new Product { Id = Guid.NewGuid(), Name = "Super Smash Bros. Ultimate", Platform = Platforms.Nintendo, TotalRating = 8.36M },
                new Product { Id = Guid.NewGuid(), Name = "Super Mario Odyssey", Platform = Platforms.Nintendo, TotalRating = 8.43M },
                new Product { Id = Guid.NewGuid(), Name = "Animal Crossing", Platform = Platforms.Nintendo, TotalRating = 7.55M },
                new Product { Id = Guid.NewGuid(), Name = "Dota 2", Platform = Platforms.PC, TotalRating = 7.64M },
                new Product { Id = Guid.NewGuid(), Name = "CS GO", Platform = Platforms.PC, TotalRating = 8.27M },
                new Product { Id = Guid.NewGuid(), Name = "Overwatch", Platform = Platforms.PC, TotalRating = 7.72M },
                new Product { Id = Guid.NewGuid(), Name = "Half-Life", Platform = Platforms.PC, TotalRating = 6.98M },
                new Product { Id = Guid.NewGuid(), Name = "Portal 2", Platform = Platforms.PC, TotalRating = 8.56M }
            );
        }
    }
}
