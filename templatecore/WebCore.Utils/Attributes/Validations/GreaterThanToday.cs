using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Utils.Attributes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class GreaterThanTodayAttribute : BaseValidation, IClientModelValidator
    {

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            string errorMessage = ErrorMessage;
            MergeAttribute(context.Attributes, "data-val-greaterthantoday", errorMessage);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            return DateTime.Now < (DateTime)value;
        }
    }
}
