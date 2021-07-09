using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.DAL.Enums;

namespace WebApp.Web.Filters
{
    public class GamesSelectingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tryGetGame = context.ActionArguments.TryGetValue(nameof(GameSelectingDTO), out var gameSelectiongDTO);

            if (!tryGetGame)
            {
                SendBadRequest(context, nameof(GameSelectingDTO));
                return;
            }

            var game = (GameSelectingDTO)gameSelectiongDTO;

            if(game.FilterParameter is not GamesSelectingConstants.FilterByGenre &&
               game.FilterParameter is not GamesSelectingConstants.FilterByAge &&
               !string.IsNullOrEmpty(game.FilterParameter))
            {
                SendBadRequest(context, GamesSelectingConstants.FilterParameter);
                return;
            }

            var filterParameterValueCastResult = game.FilterParameter switch
            {
                GamesSelectingConstants.FilterByGenre => Enum.TryParse<GamesGenres>(game.FilterParameterValue, out var gamesGenre),
                GamesSelectingConstants.FilterByAge => Enum.TryParse<GamesRating>(game.FilterParameterValue, out var gamesRating),
                _ => true
            };

            if (!filterParameterValueCastResult)
            {
                SendBadRequest(context, GamesSelectingConstants.FilterParameterValue);
                return;
            }

            if (!string.IsNullOrEmpty(game.SortField) &&
                game.SortField is not GamesSelectingConstants.SortByRating &&
                game.SortField is not GamesSelectingConstants.SortByPrice)
            {
                SendBadRequest(context, GamesSelectingConstants.SortField);
                return;
            }

            if (!string.IsNullOrEmpty(game.OrderType) &&
                game.OrderType is not GamesSelectingConstants.OrderTypeAsc &&
                game.OrderType is not GamesSelectingConstants.OrderTypeDesc)
            {
                SendBadRequest(context, GamesSelectingConstants.OrderType);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static void SendBadRequest(ActionExecutingContext context, string message)
            => context.Result = new BadRequestObjectResult(GetErrorMessage(message));

        private static string GetErrorMessage(string parameter)
            => $"Invalid value of {parameter}";
    }
}
