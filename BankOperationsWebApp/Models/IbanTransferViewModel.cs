using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankOperationsWebApp.Models
{
    public class IbanTransferViewModel
    {
        [Required]
        [IbanNumber]
        [Display(Name = "IBAN")]
        public string IbanNumber { get; set; }

        [Required]
        [Range(50, 1000)]
        [Display(Name = "Count")]
        public int Count { get; set; }
    }
}
