using WebApp.BLL.Constants;
using WebApp.BLL.Helpers;
using Xunit;

namespace UnitTests.Helpers
{
    public class GameSelectionHelperTests
    {
        [Theory]
        [InlineData(GamesSelectionConstants.FilterByGenre, "Action")]
        [InlineData(GamesSelectionConstants.FilterByAge, "12")]
        public void Get_FilterExpressionPositive_ReturnExpressionForSelecting(string filter, string value)
        {
            //Arrange
            var gameSelectionHelper = new GameSelectionHelper();

            //Act
            var resultGetExpression = gameSelectionHelper.GetFilterExpression(filter, value);

            //Assert
            Assert.NotNull(resultGetExpression);
        }

        [Theory]
        [InlineData("A", "A")]
        public void Get_FilterExpressionNegative_ReturnExpressionForSelecting(string filter, string value)
        {
            //Arrange
            var gameSelectionHelper = new GameSelectionHelper();

            //Act
            var resultGetExpression = gameSelectionHelper.GetFilterExpression(filter, value);

            //Assert
            Assert.Null(resultGetExpression);
        }

        [Theory]
        [InlineData(GamesSelectionConstants.SortByRating)]
        [InlineData(GamesSelectionConstants.SortByPrice)]
        [InlineData(null)]
        public void Get_SortExpressionPositive_ReturnExpressionForSorting(string sort)
        {
            //Arrange
            var gameSelectionHelper = new GameSelectionHelper();

            //Act
            var resultGetExpression = gameSelectionHelper.GetSortExpression(sort);

            //Assert
            Assert.NotNull(resultGetExpression);
        }
    }
}
