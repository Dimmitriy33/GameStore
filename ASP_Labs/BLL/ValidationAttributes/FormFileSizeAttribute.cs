using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.ValidationAttributes
{
    public class FormFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly int? _minFileSize = null;
        public FormFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public FormFileSizeAttribute(int minFileSize, int maxFileSize)
        {
            _maxFileSize = maxFileSize;
            _minFileSize = minFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is { _maxFileSize} bytes.");
            }

            if(_minFileSize != null)
            {
                if (file.Length < _minFileSize)
                {
                    return new ValidationResult($"Minimum allowed file size is { _minFileSize} bytes.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
