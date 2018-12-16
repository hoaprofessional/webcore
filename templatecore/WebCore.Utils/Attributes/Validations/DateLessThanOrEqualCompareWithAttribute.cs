using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Utils.Attributes.Validations
{
    public class DateLessThanOrEqualCompareWithAttribute : BaseValidation, IClientModelValidator
    {
        public string PropertyName { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            string errorMessage = ErrorMessage;
            MergeAttribute(context.Attributes, "data-val-datelessthanorequalwattr", errorMessage);
            MergeAttribute(context.Attributes, "data-val-datelessthanorequalwattr-property-compare", PropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            object instance = validationContext.ObjectInstance;
            Type instanceType = validationContext.ObjectType;
            object obj = instanceType.GetProperty(PropertyName).GetValue(instance);
            if (obj == null)
            {
                return ValidationResult.Success;
            }
            DateTime valueCompareWith = (DateTime)obj;
            if ((DateTime)value > valueCompareWith)
            {
                return new ValidationResult(ErrorMessage);
            }
            return base.IsValid(value, validationContext);
        }
    }
}
