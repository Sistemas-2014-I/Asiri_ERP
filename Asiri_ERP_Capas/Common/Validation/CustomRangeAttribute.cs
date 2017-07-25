using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Paciente.Models.Validation
{
    public class CustomRangeAttribute : RangeAttribute , IClientValidatable
    {
        private double _Minimun;
        private double _Maxmun;
        private string _message;
        public CustomRangeAttribute(double minimun , double maxmun , string message)
            :base(minimun, maxmun)
        {
            _Minimun = minimun;
            _Maxmun = maxmun;
            _message = message;
        }


        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    return base.IsValid(value, validationContext);
        //}

        public override bool IsValid(object value)
        {
            if (value==null)
            {
                return true;
            }
            double valueObject;
            Double.TryParse(Convert.ToString(value), out valueObject);
            return _Minimun <= valueObject && valueObject <= _Maxmun;
        }

        public override string FormatErrorMessage(string name)
        {
            return _message;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata , ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("range", string.Format("{0}~{1}", Minimum, Maximum));
            rule.ValidationType = "customrange";
            yield return rule;
        }




    }
}