using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
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
            var testStr = Encoding.UTF8.GetBytes("Easy peasy lemon squeezy");
            IFormFile file = new FormFile(new MemoryStream(testStr), 0, testStr.Length, "Data", "Toster.txt");

            var attrib = new FormFileSizeAttribute(FilesConstants.defaultMinFileSize, FilesConstants.defaultMaxFileSize);

            //Act
            var result = attrib.IsValid(file);

            //Assert
            Assert.True(result);
        }
    }
}
