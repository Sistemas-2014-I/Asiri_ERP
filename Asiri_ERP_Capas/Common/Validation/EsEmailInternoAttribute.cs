using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Paciente.Models.Validation
{
    public class EsEmailInternoAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string[] _emails;

        public EsEmailInternoAttribute(params string[] emails)
        {
            _emails = emails;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && _emails.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            return null;
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
                string.Join(",", _emails));
            modelClientValidationRule.ValidationType = "isemailinterno";
            yield return modelClientValidationRule;
        }

    }
}