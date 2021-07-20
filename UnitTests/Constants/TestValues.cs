using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace UnitTests.Constants
{
    public static class TestValues
    {
        public const string TestId = "37b73959-85cd-41e8-4251-08d945d5ba96";
        public const string TestUsername = "string";
        public const string TestAddressDelivery = "ulica Pervaya, dom 21";
        public const string TestPhoneNumber = "+375292929292";
        public const string TestConcurrencyStamp = "6f8ad6e6-653a-45ed-b784-9ab63141732b";
        public const string TestPassword1 = "Skolko1000minus7";
        public const string TestPassword2 = "SkolkoTisyachaMinus7";

        public const string TestToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRmYzg4OWFjLWYyYWMtNDhjMC0xYTBmLTA4ZDkzZDcxNGJmMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InN0cmluZ0BnbWFpbC5jb20iLCJuYmYiOjE2MjU4MzM1NDMsImV4cCI6MTYyNTgzNzE0MywiaXNzIjoid2ViQXBwIiwiYXVkIjoid2ViQXBwLWF1ZGllbmNlIn0.h_4YvSy-ACw8aJNlRKRViBTTPFfxDSKZgZC7e52NvpU";

        public static readonly Guid TestGuid1 = Guid.Parse("a76d6bde-c48c-4dcb-b80a-7c6edce28c74");
        public static readonly Guid TestGuid2 = Guid.Parse("37b73959-85cd-41e8-4251-08d945d5ba96");

        public static Product TestProduct1 = new Product { Id = new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"), Name = "FIFA 2020", Platform = Platforms.Playstation, TotalRating = 7.32, Genre = GamesGenres.Esports, Rating = GamesRating.Rating0, Logo = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", Background = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", Price = 10, Count = 1090 };
        public static Product TestProduct2 = new Product { Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"), Name = "Tanki 2020", Platform = Platforms.Mobile, TotalRating = 7.12, Genre = GamesGenres.Esports, Rating = GamesRating.Rating0, Logo = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", Background = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", Price = 10, Count = 1090 };

        public static readonly GameRequestDTO TestGameRequestDTO = new GameRequestDTO { Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"), Name = "Tanki 2020", Platform = Platforms.Mobile, TotalRating = 7.12, Genre = GamesGenres.Esports, Rating = GamesRating.Rating0, Logo = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Easy peasy lemon squeezy")), 0, Encoding.UTF8.GetBytes("Easy peasy lemon squeezy").Length, "Data", "Toster.txt"), Background = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Easy peasy lemon squeezy")), 0, Encoding.UTF8.GetBytes("Easy peasy lemon squeezy").Length, "Data", "Toster.txt"), Price = 10, Count = 1090, IsDeleted = false };
        public static readonly GameResponseDTO TestGameResponseDTO = new GameResponseDTO { Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"), Name = "Tanki 2020", Platform = Platforms.Mobile, TotalRating = 7.12, Genre = GamesGenres.Esports, Rating = GamesRating.Rating0, Logo = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", Background = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", Price = 10, Count = 1090, IsDeleted = false };

        public static readonly GameSelectionDTO TestGameSelectionDTO = new GameSelectionDTO { FilterType = GamesSelectionConstants.FilterByGenre, FilterValue = GamesGenres.Action.ToString(), SortField = GamesSelectionConstants.SortByRating, OrderType = OrderType.Asc.ToString() };

        public static readonly ApplicationUser TestUser = new() { Id = TestGuid1, UserName = TestUsername, AddressDelivery = TestAddressDelivery, PhoneNumber = TestPhoneNumber, ConcurrencyStamp = TestConcurrencyStamp };
    }
}
