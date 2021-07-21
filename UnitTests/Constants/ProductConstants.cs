using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace UnitTests.Constants
{
    public static class ProductConstants
    {
        public static readonly IFormFile TestFormFile = new FormFile(
                new MemoryStream(Encoding.UTF8.GetBytes("Easy peasy lemon squeezy")),
                0,
                Encoding.UTF8.GetBytes("Easy peasy lemon squeezy").Length,
                "Data",
                "Toster.txt"
            );

        public static readonly string TestLink = @"https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png";

        public static Product TestProduct1 = new()
        {
            Id = new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"),
            Name = "FIFA 2020",
            Platform = Platforms.Playstation,
            TotalRating = 7.32,
            Genre = GamesGenres.Esports,
            Rating = GamesRating.Rating0,
            Logo = TestLink,
            Background = TestLink,
            Price = 10,
            Count = 1090
        };

        public static Product TestProduct2 = new()
        {
            Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"),
            Name = "Tanki 2020",
            Platform = Platforms.Mobile,
            TotalRating = 7.12,
            Genre = GamesGenres.Esports,
            Rating = GamesRating.Rating0,
            Logo = TestLink,
            Background = TestLink,
            Price = 10,
            Count = 1090
        };

        public static readonly GameRequestDTO TestGameRequestDTO = new()
        {
            Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"),
            Name = "Tanki 2020",
            Platform = Platforms.Mobile,
            TotalRating = 7.12,
            Genre = GamesGenres.Esports,
            Rating = GamesRating.Rating0,
            Logo = TestFormFile,
            Background = TestFormFile,
            Price = 10,
            Count = 1090,
            IsDeleted = false
        };

        public static readonly GameResponseDTO TestGameResponseDTO = new()
        {
            Id = new Guid("37b73959-85cd-41e8-4251-08d945d5ba96"),
            Name = "Tanki 2020",
            Platform = Platforms.Mobile,
            TotalRating = 7.12,
            Genre = GamesGenres.Esports,
            Rating = GamesRating.Rating0,
            Logo = TestLink,
            Background = TestLink,
            Price = 10,
            Count = 1090,
            IsDeleted = false
        };

        public static readonly GameSelectionDTO TestGameSelectionDTO = new()
        {
            FilterType = GamesSelectionConstants.FilterByGenre,
            FilterValue = GamesGenres.Action.ToString(),
            SortField = GamesSelectionConstants.SortByRating,
            OrderType = OrderType.Asc.ToString()
        };

        public static List<Product> ProductsList = new()
        {
            TestProduct1,
            TestProduct2
        };
    }
}
