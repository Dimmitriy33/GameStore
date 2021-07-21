using WebApp.DAL.Entities;

namespace UnitTests.Constants
{
    public static class ProductRatingConstants
    {
        public static ProductRating TestProductRating1 = new()
        {
            ProductId = ProductConstants.TestProduct1.Id,
            Rating = ProductConstants.TestProduct1.TotalRating,
            UserId = UserConstants.TestGuid2,
            Product = ProductConstants.TestProduct1,
            User = UserConstants.TestUser
        };
    }
}
