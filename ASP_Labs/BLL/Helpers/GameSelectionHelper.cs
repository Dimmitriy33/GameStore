using System;
using System.Linq.Expressions;
using WebApp.BLL.Constants;
using WebApp.BLL.Interfaces;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Helpers
{
    public class GameSelectionHelper : IGameSelectionHelper
    {
        public Expression<Func<Product, bool>> GetFilterExpression(string filter, string value)
        {
            if(filter is GamesSelectionConstants.FilterByGenre)
            {
                var genre = Enum.Parse(typeof(GamesGenres), value);

                return t => t.Genre == (GamesGenres)genre;
            }

            if (filter is GamesSelectionConstants.FilterByAge)
            {
                var rating = Enum.Parse(typeof(GamesRating), value);
                    
                return t => t.Rating == (GamesRating)rating;
            }

            return null;
        }
        public Expression<Func<Product, object>> GetSortExpression(string sort)
            => sort switch
            {
                GamesSelectionConstants.SortByRating => t => t.TotalRating,
                GamesSelectionConstants.SortByPrice => t => t.Price,
                _ => t => t.Name
            };
    }
}
