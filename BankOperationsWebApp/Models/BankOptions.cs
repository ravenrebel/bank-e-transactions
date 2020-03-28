using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankOperationsWebApp.Models
{
    public class BankOptions
    {
        public string CountryCode { get; set; }

        public string BankMfoNumber { get; set; }

        public string BankName { get; set; }

        public string GenerateIbanNumber(string cardNumber)
        {
            long controlNumber = 98 - Convert.ToInt64(cardNumber) * 100 % 97;
            return CountryCode + controlNumber + BankMfoNumber + cardNumber.PadLeft(19, '0');
        }
    }
}
