using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FinalLaboratorio4.Validators
{
    public class MinFileSizeAttribute : ValidationAttribute
    {
        private readonly int _minFileSize;

        public MinFileSizeAttribute()
        {
            _minFileSize = 0;
        }

        public MinFileSizeAttribute(int minFileSize)
        {
            _minFileSize = minFileSize;
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            IFormFile file = value as IFormFile;

            if (file != null && file.Length < _minFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"El tamaño de archivo mínimo permitido es de { _minFileSize} bytes.";
        }
    }
}
