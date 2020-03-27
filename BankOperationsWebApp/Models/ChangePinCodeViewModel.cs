using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankOperationsWebApp.Models
{
    public class ChangePinCodeViewModel
    {
        [Required]
        [RegularExpression(@"\d*", ErrorMessage = "PIN must contain only numbers")]
        [StringLength(4, ErrorMessage = "{0} length must be {1}.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Old PIN")]
        public string PinCode { get; set; }

        [Required]
        [RegularExpression(@"\d*", ErrorMessage = "PIN must contain only numbers")]
        [StringLength(4, ErrorMessage = "{0} length must be {1}.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "New PIN")]
        public string NewPinCode { get; set; }
    }
}
