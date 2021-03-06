using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApp.BLL.ValidationAttributes
{
    public class FormFileSizeAttribute : ValidationAttribute
    {
        private readonly int? _maxFileSize = null;
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

            if (!_maxFileSize.HasValue || file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage(_maxFileSize));
            }

            if (!_minFileSize.HasValue || file.Length < _minFileSize)
            {
                return new ValidationResult(GetErrorMessage(_minFileSize));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(int? fileSize)
            => $"Maximum allowed file size is { fileSize} bytes.";
    }
}
