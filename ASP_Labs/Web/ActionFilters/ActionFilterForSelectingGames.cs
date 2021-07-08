using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using WebApp.BLL.DTO;
using WebApp.DAL.Enums;

namespace WebApp.Web.ActionFilters
{
    public class ActionFilterForSelectingGames : IActionFilter
    {
        #region Constants

        private const string FilterByGenre = "Genre";
        private const string FilterByAge = "Age";

        private const string SortByRating = "Rating";
        private const string SortByPrice = "Price";

        private const string OrderTypeAsc = "Asc";
        private const string OrderTypeDesc = "Desc";

        private const string FilterParameter = "FilterParameter";
        private const string FilterParameterValue = "FilterParameterValue";
        private const string SortField = "SortField";
        private const string OrderType = "OrderType";

        #endregion

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ActionArguments.ContainsKey("GameSelectionDTO"))
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage("GameSelectionDTO"));
                return;
            }

            GameSelectionDTO game = context.ActionArguments.SingleOrDefault(p => p.Value is GameSelectionDTO).Value as GameSelectionDTO;

            bool result = false;

            if(game.FilterParameter is FilterByGenre)
            {
                GamesGenres gamesGenre;
                result = Enum.TryParse(game.FilterParameterValue, out gamesGenre);
            }
            else if(game.FilterParameter is FilterByAge)
            {
                GamesRating gamesRating;
                result = Enum.TryParse(game.FilterParameterValue, out gamesRating);
            }
            else
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage(FilterParameter));
                return;
            }

            if (result is false)
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage(FilterParameterValue));
                return;
            }

            if( game.SortField is not null &&
                game.SortField is not SortByRating &&
                game.SortField is not SortByPrice)
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage(SortField));
                return;
            }

            if( game.OrderType is not null &&
                game.OrderType is not OrderTypeAsc &&
                game.OrderType is not OrderTypeDesc)
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage(OrderType));
                return;
            }

            return;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static string GetErrorMessage(string parameter) 
            => $"Invalid value of {parameter}";
    }
}
