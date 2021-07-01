﻿using Microsoft.EntityFrameworkCore;
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

            builder
                .Property(b => b.Id)
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property(b => b.Name).IsRequired();

            builder
                .Property(b => b.Platform)
                .IsRequired();

            builder
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder
                .Property(b => b.TotalRating)
                .IsRequired()
                .HasColumnType("decimal(4, 2)");

            builder
                .Property(b => b.Genre)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder
                .Property(b => b.Rating)
                .IsRequired();

            builder
                .Property(b => b.Logo)
                .IsRequired();

            builder
                .Property(b => b.Background)
                .IsRequired();

            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.Count).IsRequired();

            builder.HasIndex(b => b.Name);
            builder.HasIndex(b => b.Platform);
            builder.HasIndex(b => b.TotalRating);
            builder.HasIndex(b => b.DateCreated);
            builder.HasIndex(b => b.Genre);
            builder.HasIndex(b => b.Rating);
            builder.HasIndex(b => b.Price);
            builder.HasIndex(b => b.Count);

            builder.HasData(
                new Product { Id = Guid.NewGuid(), Name = "FIFA 2020", Platform = Platforms.Playstation, TotalRating = 7.32M, Genre = GamesGenres.Esports, Rating = GamesRating.Rating0, Logo= @"https://ru.wikipedia.org/wiki/FIFA_(%D1%81%D0%B5%D1%80%D0%B8%D1%8F_%D0%B8%D0%B3%D1%80)#/media/%D0%A4%D0%B0%D0%B9%D0%BB:FIFA_series_logo.svg.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 10, Count=1090 },
                new Product { Id = Guid.NewGuid(), Name = "God of War", Platform = Platforms.Playstation, TotalRating = 8.31M, Genre = GamesGenres.Strategy, Rating = GamesRating.Rating16, Logo = @"https://ru.wikipedia.org/wiki/God_of_War_(%D1%81%D0%B5%D1%80%D0%B8%D1%8F_%D0%B8%D0%B3%D1%80)#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9B%D0%BE%D0%B3%D0%BE%D1%82%D0%B8%D0%BF_%D0%B8%D0%B3%D1%80%D1%8B_God_of_War.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 20, Count = 1080 },
                new Product { Id = Guid.NewGuid(), Name = "Bloodborne", Platform = Platforms.Playstation, TotalRating = 6.81M, Genre = GamesGenres.Action, Rating = GamesRating.Rating16, Logo = @"https://ru.wikipedia.org/wiki/Bloodborne#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9E%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0_Bloodborne.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 30, Count = 1070 },
                new Product { Id = Guid.NewGuid(), Name = "Among Us", Platform = Platforms.Mobile, TotalRating = 8.32M, Genre = GamesGenres.Role_Playing, Rating = GamesRating.Rating6, Logo= @"https://ru.wikipedia.org/wiki/Among_Us#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Among_Us.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 40, Count=1060 },
                new Product { Id = Guid.NewGuid(), Name = "Brawl Stars", Platform = Platforms.Mobile, TotalRating = 10M, Genre = GamesGenres.MMO, Rating = GamesRating.Rating12, Logo= @"https://ru.wikipedia.org/wiki/Brawl_Stars#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Brawl_Stars.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 50, Count=1050 },
                new Product { Id = Guid.NewGuid(), Name = "Fortnite", Platform = Platforms.Xbox, TotalRating = 8.88M, Genre = GamesGenres.Action, Rating = GamesRating.Rating6, Logo= @"https://ru.wikipedia.org/wiki/Fortnite#/media/%D0%A4%D0%B0%D0%B9%D0%BB:FortniteLogo.svg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 60, Count=1040 },
                new Product { Id = Guid.NewGuid(), Name = "Minecraft", Platform = Platforms.Xbox, TotalRating = 9.51M, Genre = GamesGenres.Simulation, Rating = GamesRating.Rating6, Logo= @"https://ru.wikipedia.org/wiki/Minecraft#/media/%D0%A4%D0%B0%D0%B9%D0%BB:MinecraftLogo.svg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 70, Count=1030 },
                new Product { Id = Guid.NewGuid(), Name = "Forza Horizon 4", Platform = Platforms.Xbox, TotalRating = 9.49M, Genre = GamesGenres.Action, Rating = GamesRating.Rating6, Logo= @"https://ru.wikipedia.org/wiki/Forza_Horizon_4#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Forza_Horizon_4_coverart.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 80, Count=1020 },
                new Product { Id = Guid.NewGuid(), Name = "Super Smash Bros. Ultimate", Platform = Platforms.Nintendo, TotalRating = 8.36M, Genre = GamesGenres.Action, Rating = GamesRating.Rating6, Logo= @"https://ru.wikipedia.org/wiki/Super_Smash_Bros._Ultimate#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Super_Smash_Bros._Ultimate.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 90, Count=1010 },
                new Product { Id = Guid.NewGuid(), Name = "Super Mario Odyssey", Platform = Platforms.Nintendo, TotalRating = 8.43M, Genre = GamesGenres.Strategy, Rating = GamesRating.Rating0, Logo= @"https://ru.wikipedia.org/wiki/Super_Mario_Odyssey#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Super_Mario_Odyssey_box.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 100, Count=100 },
                new Product { Id = Guid.NewGuid(), Name = "Animal Crossing", Platform = Platforms.Nintendo, TotalRating = 7.55M, Genre = GamesGenres.Adventure, Rating = GamesRating.Rating0, Logo= @"https://ru.wikipedia.org/wiki/Animal_Crossing#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Animal_Crossing_Logo.png", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 110, Count=1600 },
                new Product { Id = Guid.NewGuid(), Name = "Dota 2", Platform = Platforms.PC, TotalRating = 7.64M, Genre = GamesGenres.Strategy, Rating = GamesRating.Rating12, Logo= @"https://ru.wikipedia.org/wiki/Dota_2#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9E%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0_Dota_2.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 120, Count=1500 },
                new Product { Id = Guid.NewGuid(), Name = "CS GO", Platform = Platforms.PC, TotalRating = 8.27M, Genre = GamesGenres.MMO, Rating = GamesRating.Rating16, Logo= @"https://ru.wikipedia.org/wiki/Counter-Strike:_Global_Offensive#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9B%D0%BE%D0%B3%D0%BE%D1%82%D0%B8%D0%BF_Counter-Strike_Global_Offensive.svg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 130, Count=1400 },
                new Product { Id = Guid.NewGuid(), Name = "Overwatch", Platform = Platforms.PC, TotalRating = 7.72M, Genre = GamesGenres.MMO, Rating = GamesRating.Rating12, Logo= @"https://en.wikipedia.org/wiki/Overwatch_(video_game)#/media/File:Overwatch_cover_art.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 140, Count=1300 },
                new Product { Id = Guid.NewGuid(), Name = "Half-Life", Platform = Platforms.PC, TotalRating = 6.98M, Genre = GamesGenres.Strategy, Rating = GamesRating.Rating16, Logo= @"https://ru.wikipedia.org/wiki/Half-Life_2#/media/%D0%A4%D0%B0%D0%B9%D0%BB:HL2box.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 150, Count=1200 },
                new Product { Id = Guid.NewGuid(), Name = "Portal 2", Platform = Platforms.PC, TotalRating = 8.56M, Genre = GamesGenres.Strategy, Rating = GamesRating.Rating0, Logo= @"https://ru.wikipedia.org/wiki/Portal#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Portal_boxcover.jpg", Background= @"https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", Price = 160, Count=1100 }
            );
        }
    }
}