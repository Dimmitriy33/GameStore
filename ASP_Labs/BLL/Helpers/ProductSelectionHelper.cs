using System;
using System.Linq.Expressions;
using WebApp.BLL.Interfaces;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Helpers
{
    public class ProductSelectionHelper : IProductSelectionHelper
    {
        #region Constants

        private const string FilterByGenre = "Genre";
        private const string FilterByAge = "Age";

        private const string SortByRating = "Rating";
        private const string SortByPrice = "Price";

        #endregion

        public Expression<Func<Product, bool>> GetFilterExpression(string filter, string value)
        {
            if(filter is FilterByGenre)
            {
                GamesGenres genre;
                var result = Enum.TryParse(value, out genre);

                if(result)
                {
                    return t => t.Genre == genre;
                }

                return null;
            }
            else if (filter is FilterByAge)
            {
                GamesRating rating;
                var result = Enum.TryParse(value, out rating);

                if(result)
                {
                    return t => t.Rating == rating;
                }

                return null;
            }

            return null;
        }

        public Expression<Func<Product, object>> GetSortExpression(string sort)
        {
            if (sort is SortByRating)
            {
                return t => t.TotalRating;
            }
            else if (sort is SortByPrice)
            {
                return t => t.Price;
            }

            return t => t.Name;
        }

        public OrderType GetOrderType(string value)
        {
            if(value == OrderType.Desc.ToString())
            {
                return OrderType.Desc;
            }

            return OrderType.Asc;
        }
    }
}
