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
            builder.Property("TotalRating");
            builder.Property("Platform");
            builder.Property(b => b.DateCreated).HasDefaultValueSql("getdate()");

            builder.HasData(
                new Product { Id = Guid.Parse("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"), Name = "FIFA 2020", Platform = Platforms.Playstation, TotalRating = 7.32 },
                new Product { Id = Guid.Parse("7bad0c87-edd2-4a23-aade-aaff2e19f54f"), Name = "God of War", Platform = Platforms.Playstation, TotalRating = 8.31 },
                new Product { Id = Guid.Parse("67550e04-f55d-40c6-bd72-3cbffef51317"), Name = "Bloodborne", Platform = Platforms.Playstation, TotalRating = 6.81 },
                new Product { Id = Guid.Parse("5acb3515-9b43-4b80-9338-1ff4e0dc972b"), Name = "Among Us", Platform = Platforms.Mobile, TotalRating = 8.32 },
                new Product { Id = Guid.Parse("ea803243-ed41-49e9-9670-29619e3e4961"), Name = "Brawl Stars", Platform = Platforms.Mobile, TotalRating = 10 },
                new Product { Id = Guid.Parse("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"), Name = "Fortnite", Platform = Platforms.Xbox, TotalRating = 8.88 },
                new Product { Id = Guid.Parse("0e70d082-9558-48aa-84a8-5a34ac95af08"), Name = "Minecraft", Platform = Platforms.Xbox, TotalRating = 9.51 },
                new Product { Id = Guid.Parse("50973345-c933-4098-9513-3c16d82dcc0a"), Name = "Forza Horizon 4", Platform = Platforms.Xbox, TotalRating = 9.49 },
                new Product { Id = Guid.Parse("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"), Name = "Super Smash Bros. Ultimate", Platform = Platforms.Nintendo, TotalRating = 8.36 },
                new Product { Id = Guid.Parse("82e27206-1bfd-4d62-a6bf-be44ad030b25"), Name = "Super Mario Odyssey", Platform = Platforms.Nintendo, TotalRating = 8.43 },
                new Product { Id = Guid.Parse("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"), Name = "Animal Crossing", Platform = Platforms.Nintendo, TotalRating = 7.55 },
                new Product { Id = Guid.Parse("1952c825-184a-40e9-8864-80358a9f1da6"), Name = "Dota 2", Platform = Platforms.PC, TotalRating = 7.64 },
                new Product { Id = Guid.Parse("d4b797a8-2f74-446e-ad0c-71dad9e37e59"), Name = "CS GO", Platform = Platforms.PC, TotalRating = 8.27 },
                new Product { Id = Guid.Parse("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"), Name = "Overwatch", Platform = Platforms.PC, TotalRating = 7.72 },
                new Product { Id = Guid.Parse("00189d6e-ed62-482b-a4d9-335dfa68d58e"), Name = "Half-Life", Platform = Platforms.PC, TotalRating = 6.98 },
                new Product { Id = Guid.Parse("1ad798c4-da8c-4e87-a020-9272e4e71d2b"), Name = "Portal 2", Platform = Platforms.PC, TotalRating = 8.56 }
            );
        }
    }
}
