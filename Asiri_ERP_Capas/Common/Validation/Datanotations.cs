using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Common.Validation
{
    public class AgeTrueValidation : ValidationAttribute //, IClientValidatable
    {

        public string fecNow = DateTime.Now.ToString();
        private string _message;

        public AgeTrueValidation(string message)
        {
            _message = message;
        }

        public override string FormatErrorMessage(string name)
        {
            return _message+fecNow;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            var fecNow = DateTime.Now;
            var fecNaci = ((DateTime)value);
            if (fecNow >= fecNaci)
            {
                return null;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }
        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    //here we will set validation rule for client side validation
        //    var rule = new ModelClientValidationRule
        //    {
        //        ErrorMessage = FormatErrorMessage(metadata.DisplayName),
        //        ValidationType = "minagevalidation" //must be all in lower case , spacial char not allowed in the validation type name
        //    };
        //    rule.ValidationParameters["minage"] = "";
        //    yield return rule;
        //}

    }


    public class IsNumDocAttribute : ValidationAttribute //, IClientValidatable
    {
        //public int MinimumValue { get; set; }
        public IsNumDocAttribute()
        {
            //this.MinimumValue = minimum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                var model = (Common.RHUt09_persona)validationContext.ObjectInstance;
                int idTipoDoc = Convert.ToInt16(model.idTipoDocIdentidad);
                string numDoc = value.ToString();
                int numValue;
                switch (idTipoDoc)
                {
                    case 1:
                   
                        bool parsed = Int32.TryParse(numDoc, out numValue);

                        if (numDoc.ToString().Length == 8 && parsed==true)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            //ValidationResult a;
                            //a.ErrorMessage = 
                            return new ValidationResult(FormatErrorMessage("La longitud no debe ser mayor ni menor a 8 dígitos y debe ser un número."));
                        }
                    //break;
                    case 2:
                        if (numDoc.ToString().Length >= 5 && numDoc.ToString().Length <= 12)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult(FormatErrorMessage("La longitud no debe ser menor a 5 caracteres y mayor a 12."));
                        }
                    //break;
                    case 3:
                        Int64 numValue2;
                        bool parsed3 = Int64.TryParse(numDoc, out numValue2);
                        if (numDoc.ToString().Length == 11 && parsed3==true)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult(FormatErrorMessage("La longitud no debe ser mayor ni menor a 11 dígitos y debé ser un número."));
                        }
                    case 4:
                        if (numDoc.ToString().Length >= 5 && numDoc.ToString().Length <= 12)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult(FormatErrorMessage("La longitud no debe ser menor a 5 caracteres ni mayor a 12."));
                        }
                    default:
                        if (numDoc.ToString().Length >= 8 && numDoc.ToString().Length <= 15)
                        {
                            return ValidationResult.Success;
                        }
                        else
                        {
                            return new ValidationResult(FormatErrorMessage("La longitud no debe ser menor a 5 dígitos ni mayor a 15."));
                        }
                        //break;
                }
            }
            return new ValidationResult(FormatErrorMessage("El campo N° de Documento es requerido."));
        }

        //public IEnumerable<ModelClientValidationRule>GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    var modelClientValidationRule = new ModelClientValidationRule
        //    {
        //        ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
        //    };
        //    //modelClientValidationRule.ValidationParameters.Add("emails",string.Join(",", MinimumValue));
        //    modelClientValidationRule.ValidationType = "issnumdocattribute";
        //    yield return modelClientValidationRule;
        //}
        ////MESSAGE

        public override string FormatErrorMessage(string name)
        {
            return name;
        }

        //FormatErrorMessage(metadata.GetDisplayName());
    }

}