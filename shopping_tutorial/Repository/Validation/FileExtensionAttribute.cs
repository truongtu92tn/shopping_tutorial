using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace shopping_tutorial.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions = { "jpg", "jpeg", "png", "gif" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();

                if (!_validExtensions.Contains(extension))
                {
                    return new ValidationResult($"Allowed extensions are: {string.Join(", ", _validExtensions)}");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("File is required.");
        }
    }
}
