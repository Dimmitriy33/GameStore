using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.DAL.Enums;

namespace WebApp.Web.Filters
{
    public class GamesSelectionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tryGetGame = context.ActionArguments.TryGetValue(nameof(GameSelectionDTO), out var gameSelectiongDTO);

            if (!tryGetGame)
            {
                SendBadRequest(context, nameof(GameSelectionDTO));
                return;
            }

            var game = (GameSelectionDTO)gameSelectiongDTO;

            if(game.FilterParameter is not GamesSelectionConstants.FilterByGenre &&
               game.FilterParameter is not GamesSelectionConstants.FilterByAge &&
               !string.IsNullOrEmpty(game.FilterParameter))
            {
                SendBadRequest(context, GamesSelectionConstants.FilterParameter);
                return;
            }

            var filterParameterValueCastResult = game.FilterParameter switch
            {
                GamesSelectionConstants.FilterByGenre => Enum.TryParse<GamesGenres>(game.FilterParameterValue, out var gamesGenre),
                GamesSelectionConstants.FilterByAge => Enum.TryParse<GamesRating>(game.FilterParameterValue, out var gamesRating),
                _ => true
            };

            if (!filterParameterValueCastResult)
            {
                SendBadRequest(context, GamesSelectionConstants.FilterParameterValue);
                return;
            }

            if (!string.IsNullOrEmpty(game.SortField) &&
                game.SortField is not GamesSelectionConstants.SortByRating &&
                game.SortField is not GamesSelectionConstants.SortByPrice)
            {
                SendBadRequest(context, GamesSelectionConstants.SortField);
                return;
            }

            if (!string.IsNullOrEmpty(game.OrderType) &&
                game.OrderType is not GamesSelectionConstants.OrderTypeAsc &&
                game.OrderType is not GamesSelectionConstants.OrderTypeDesc)
            {
                SendBadRequest(context, GamesSelectionConstants.OrderType);
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
