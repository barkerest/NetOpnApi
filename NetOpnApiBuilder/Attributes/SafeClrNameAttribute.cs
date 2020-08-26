using System.ComponentModel.DataAnnotations;
using NetOpnApiBuilder.Extensions;

namespace NetOpnApiBuilder.Attributes
{
    public class SafeClrNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var s = value as string;
            
            // ignore null/blank, leave that for Required attribute to handle.
            if (string.IsNullOrEmpty(s)) return ValidationResult.Success;
            
            if (!s.IsSafeClrName(out var reason))
            {
                return new ValidationResult(reason);
            }
            
            return ValidationResult.Success;
        }
    }
}
