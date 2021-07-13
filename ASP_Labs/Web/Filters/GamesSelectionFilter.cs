using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Helpers;
using WebApp.DAL.Enums;

namespace WebApp.Web.Filters
{
    public class GamesSelectionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tryGetGame = context.ActionArguments.TryGetValue(nameof(GameSelectionDTO), out var gameSelectionDTO);

            if (!tryGetGame)
            {
                FiltersHelper.SendBadRequest(context, nameof(GameSelectionDTO));
                return;
            }

            var game = (GameSelectionDTO)gameSelectionDTO;

            if(!string.IsNullOrEmpty(game.FilterType) &&
               game.FilterType is not GamesSelectionConstants.FilterByGenre &&
               game.FilterType is not GamesSelectionConstants.FilterByAge)
            {
                FiltersHelper.SendBadRequest(context, GamesSelectionConstants.FilterType);
                return;
            }

            var filterValueCastResult = game.FilterType switch
            {
                GamesSelectionConstants.FilterByGenre => Enum.TryParse<GamesGenres>(game.FilterValue, out _),
                GamesSelectionConstants.FilterByAge => Enum.TryParse<GamesRating>(game.FilterValue, out _),
                _ => true
            };

            if (!filterValueCastResult)
            {
                FiltersHelper.SendBadRequest(context, GamesSelectionConstants.FilterValue);
                return;
            }

            if (!string.IsNullOrEmpty(game.SortField) &&
                game.SortField is not GamesSelectionConstants.SortByRating &&
                game.SortField is not GamesSelectionConstants.SortByPrice)
            {
                FiltersHelper.SendBadRequest(context, GamesSelectionConstants.SortField);
                return;
            }

            if (!string.IsNullOrEmpty(game.OrderType) &&
                game.OrderType is not GamesSelectionConstants.OrderTypeAsc &&
                game.OrderType is not GamesSelectionConstants.OrderTypeDesc)
            {
                FiltersHelper.SendBadRequest(context, GamesSelectionConstants.OrderType);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
