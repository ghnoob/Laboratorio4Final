using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FinalLaboratorio4.Validators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            IFormFile file = value as IFormFile;

            if (file != null && file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"El tamaño de archivo máximo permitido es de { _maxFileSize} bytes.";
        }
    }
}
