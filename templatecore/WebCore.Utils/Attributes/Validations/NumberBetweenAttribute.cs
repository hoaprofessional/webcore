using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Utils.Attributes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NumberBetweenAttribute : BaseValidation, IClientModelValidator
    {
        public double NumberMinValue { get; set; }
        public double NumberMaxValue { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            string errorMessage = ErrorMessage;
            MergeAttribute(context.Attributes, "data-val-numberbetween", errorMessage);
            MergeAttribute(context.Attributes, "data-val-numberbetween-from", NumberMinValue.ToString());
            MergeAttribute(context.Attributes, "data-val-numberbetween-to", NumberMaxValue.ToString());
        }

        public override bool IsValid(object value)
        {
            return (double)value >= NumberMinValue && (double)value <= NumberMaxValue;
        }

        
    }
}
