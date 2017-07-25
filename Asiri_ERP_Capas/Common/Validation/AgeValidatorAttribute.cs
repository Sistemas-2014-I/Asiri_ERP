using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Asiri_ERP_Paciente.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class AgeValidatorAttribute : ValidationAttribute, IClientValidatable
    {
        public int MinimumValue { get; set; }
        public AgeValidatorAttribute(int minimum)
        {
            this.MinimumValue = minimum;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var valueToCompare = int.Parse(value.ToString());
                if (valueToCompare > MinimumValue)
                    return true;
                return false;
            }
            return false;
        }

        public IEnumerable<ModelClientValidationRule>
            GetClientValidationRules(
                ModelMetadata metadata,
                ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            modelClientValidationRule.ValidationParameters.Add("emails",
                string.Join(",", MinimumValue));
            modelClientValidationRule.ValidationType = "isemailinterno";
            yield return modelClientValidationRule;
        }
    }
}