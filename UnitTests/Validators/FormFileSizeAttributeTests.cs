using UnitTests.Constants;
using WebApp.BLL.Constants;
using WebApp.BLL.ValidationAttributes;
using Xunit;

namespace UnitTests.Validators
{
    public class FormFileSizeAttributeTests
    {
        [Fact]
        public void Check_FormFileSizeIsValid_ReturnBool()
        {
            //Arrange
            var file = ProductConstants.TestFormFile;

            var attrib = new FormFileSizeAttribute(FilesConstants.defaultMinFileSize, FilesConstants.defaultMaxFileSize);

            //Act
            var result = attrib.IsValid(file);

            //Assert
            Assert.True(result);
        }
    }
}
