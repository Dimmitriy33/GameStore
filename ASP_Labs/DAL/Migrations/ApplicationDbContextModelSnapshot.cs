﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.DAL;

namespace WebApp.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApp.DAL.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("WebApp.DAL.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AddressDelivery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebApp.DAL.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRating")
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.HasIndex("Count");

                    b.HasIndex("DateCreated");

                    b.HasIndex("Genre");

                    b.HasIndex("Name");

                    b.HasIndex("Platform");

                    b.HasIndex("Price");

                    b.HasIndex("Rating");

                    b.HasIndex("TotalRating");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1090,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Esports",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png",
                            Name = "FIFA 2020",
                            Platform = 1,
                            Price = 10m,
                            Rating = 0,
                            TotalRating = 7.32m
                        },
                        new
                        {
                            Id = new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1080,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229190/ASP_Labs/D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF__D0_B8_D0_B3_D1_80_D1_8B_God_of_War_mgno9l.png",
                            Name = "God of War",
                            Platform = 1,
                            Price = 20m,
                            Rating = 16,
                            TotalRating = 8.31m
                        },
                        new
                        {
                            Id = new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1070,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229196/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Bloodborne_zsqu5h.jpg",
                            Name = "Bloodborne",
                            Platform = 1,
                            Price = 30m,
                            Rating = 16,
                            TotalRating = 6.81m
                        },
                        new
                        {
                            Id = new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1060,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Role_Playing",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229203/ASP_Labs/Among_Us_y6hnjs.png",
                            Name = "Among Us",
                            Platform = 4,
                            Price = 40m,
                            Rating = 6,
                            TotalRating = 8.32m
                        },
                        new
                        {
                            Id = new Guid("ea803243-ed41-49e9-9670-29619e3e4961"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1050,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "MMO",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229211/ASP_Labs/Brawl_Stars_w86qfv.png",
                            Name = "Brawl Stars",
                            Platform = 4,
                            Price = 50m,
                            Rating = 12,
                            TotalRating = 10m
                        },
                        new
                        {
                            Id = new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1040,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229220/ASP_Labs/1920px-FortniteLogo.svg_kcwnbv.png",
                            Name = "Fortnite",
                            Platform = 2,
                            Price = 60m,
                            Rating = 6,
                            TotalRating = 8.88m
                        },
                        new
                        {
                            Id = new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1030,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Simulation",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229230/ASP_Labs/1920px-MinecraftLogo.svg_gamutf.png",
                            Name = "Minecraft",
                            Platform = 2,
                            Price = 70m,
                            Rating = 6,
                            TotalRating = 9.51m
                        },
                        new
                        {
                            Id = new Guid("50973345-c933-4098-9513-3c16d82dcc0a"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1020,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229084/ASP_Labs/Forza_Horizon_4_coverart_qmuz6l.jpg",
                            Name = "Forza Horizon 4",
                            Platform = 2,
                            Price = 80m,
                            Rating = 6,
                            TotalRating = 9.49m
                        },
                        new
                        {
                            Id = new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1010,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229253/ASP_Labs/Super_Smash_Bros._Ultimate_yvp3wa.png",
                            Name = "Super Smash Bros. Ultimate",
                            Platform = 5,
                            Price = 90m,
                            Rating = 6,
                            TotalRating = 8.36m
                        },
                        new
                        {
                            Id = new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 100,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229266/ASP_Labs/Super_Mario_Odyssey_box_ydaxqk.jpg",
                            Name = "Super Mario Odyssey",
                            Platform = 5,
                            Price = 100m,
                            Rating = 0,
                            TotalRating = 8.43m
                        },
                        new
                        {
                            Id = new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1600,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Adventure",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229280/ASP_Labs/Animal_Crossing_Logo_tbw4bw.png",
                            Name = "Animal Crossing",
                            Platform = 5,
                            Price = 110m,
                            Rating = 0,
                            TotalRating = 7.55m
                        },
                        new
                        {
                            Id = new Guid("1952c825-184a-40e9-8864-80358a9f1da6"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1500,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229295/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Dota_2_rd8cox.jpg",
                            Name = "Dota 2",
                            Platform = 3,
                            Price = 120m,
                            Rating = 12,
                            TotalRating = 7.64m
                        },
                        new
                        {
                            Id = new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1400,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "MMO",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229311/ASP_Labs/1920px-_D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF_Counter-Strike_Global_Offensive.svg_cg9fcb.png",
                            Name = "CS GO",
                            Platform = 3,
                            Price = 130m,
                            Rating = 16,
                            TotalRating = 8.27m
                        },
                        new
                        {
                            Id = new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1300,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "MMO",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229328/ASP_Labs/Overwatch_cover_art_whuqso.jpg",
                            Name = "Overwatch",
                            Platform = 3,
                            Price = 140m,
                            Rating = 12,
                            TotalRating = 7.72m
                        },
                        new
                        {
                            Id = new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1200,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229345/ASP_Labs/HL2box_lhx2ag.jpg",
                            Name = "Half-Life",
                            Platform = 3,
                            Price = 150m,
                            Rating = 16,
                            TotalRating = 6.98m
                        },
                        new
                        {
                            Id = new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"),
                            Background = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg",
                            Count = 1100,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Logo = "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229364/ASP_Labs/Portal_boxcover_ojqdry.jpg",
                            Name = "Portal 2",
                            Platform = 3,
                            Price = 160m,
                            Rating = 0,
                            TotalRating = 8.56m
                        });
                });

            modelBuilder.Entity("WebApp.DAL.Entities.ProductRating", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("Rating");

                    b.HasIndex("UserId");

                    b.ToTable("ProductRating");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.DAL.Entities.ProductRating", b =>
                {
                    b.HasOne("WebApp.DAL.Entities.Product", null)
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.DAL.Entities.ApplicationUser", null)
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.DAL.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("WebApp.DAL.Entities.Product", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
