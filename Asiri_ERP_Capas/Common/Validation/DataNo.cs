using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Paciente.Models.Validation
{

    //NUEVO
    //public class MyStringLengthAdapter : DataAnnotationsModelValidator<MyStringLengthAdapter>
    //{
    //    public MyStringLengthAdapter(ModelMetadata metadata, ControllerContext context, MyStringLengthAdapter attribute)
    //        : base(metadata, context, attribute)
    //    {
    //    }

    //    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
    //    {
    //        return new[] { new ModelClientValidationStringLengthRule(ErrorMessage,12, 15) };
    //    }
    //}

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MyDateAttribute : ValidationAttribute
    {
        public MyDateAttribute(int monthsSpan)
        {
            this.MonthsSpan = monthsSpan;
        }

        public int MonthsSpan { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var date = (DateTime)value;
                var now = DateTime.Now;
                var futureDate = now.AddMonths(this.MonthsSpan);

                if (now <= date && date < futureDate)
                {
                    return null;
                }
            }

            return new ValidationResult(this.FormatErrorMessage(this.ErrorMessage));
        }
    }


    [AttributeUsage(AttributeTargets.Property |
     AttributeTargets.Field, AllowMultiple = true)]
    //PARA DESPUES DEL MODEL STATE 
    sealed public class Age2ValidatorAttribute : ValidationAttribute
    {
        public int MinimumValue { get; set; }
        public Age2ValidatorAttribute(int minimum)
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
    }

    public class IsEmailInternoAttribute : RegularExpressionAttribute
    {
        public IsEmailInternoAttribute()
            : base(".+@tabconsultores\\.(com|net)$")
        {
        }
    }


    //TOUPPER

    //public class StringBinder : DefaultModelBinder
    //{
    //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        object o = base.BindModel(controllerContext, bindingContext);
    //        return o as string != null ? ((string)o).ToUpper() : o;
    //    }
    //}


    //FECHA DE NACIMIENTO
    public class DataTypeAttributeAdapter : DataAnnotationsModelValidator<DataTypeAttribute>
    {
        public DataTypeAttributeAdapter(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute)
            : base(metadata, context, attribute) { }

        public override System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (Attribute.DataType == DataType.Date)
            {
                return new[] { new ModelClientValidationDateRule(Attribute.FormatErrorMessage(Metadata.GetDisplayName())) };
            }

            return base.GetClientValidationRules();
        }
        public class ModelClientValidationDateRule : ModelClientValidationRule
        {
            public ModelClientValidationDateRule(string errorMessage)
            {
                ErrorMessage = errorMessage;
                ValidationType = "date";
            }
        }
    }

}