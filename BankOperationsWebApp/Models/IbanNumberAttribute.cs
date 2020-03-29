using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankOperationsWebApp.Models
{
    public class IbanNumberAttribute:ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Incorrect IBAN.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var iban = (string)value;
            if (iban.Length < 9) return new ValidationResult(GetErrorMessage());

            if (char.IsUpper(iban[0]) && char.IsUpper(iban[1]) && iban.Remove(0, 4).All(n => char.IsDigit(n)))
            {
                string controlSum = iban[2].ToString() + iban[3].ToString();
                int sum;
                long cardNumber;
                if (int.TryParse(controlSum, out sum) && long.TryParse(iban.Remove(0, 10), out cardNumber))
                {
                    if (98 - cardNumber * 100 % 97 == sum)
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return new ValidationResult(GetErrorMessage());
        }
    }
}
