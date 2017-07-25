using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Paciente.Models.Validation
{
    public class Age2Validation : ValidationAttribute , IClientValidatable
    {
        public int Minimunage { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            int currentAge = DateTime.Now.Year - ((DateTime)value).Year;
            if(currentAge >= Minimunage)
            {
                return null;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata , ControllerContext context )
        {
            //here we will set validation rule for client side validation
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "minagevalidation" //must be all in lower case , spacial char not allowed in the validation type name
            };
            rule.ValidationParameters["minage"] = Minimunage;
            yield return rule;
        }

    }
}