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
            long controlSum = 98 - Convert.ToInt64(cardNumber) * 100 % 97;
            return CountryCode + controlSum.ToString().PadLeft(2, '0') + BankMfoNumber + cardNumber.PadLeft(19, '0');
        }
    }
}
