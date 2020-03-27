using System.ComponentModel.DataAnnotations;

namespace BankOperationsWebApp.Models
{
    public class CardNumberTransferViewModel
    {
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [Range(50, 1000)]
        [Display(Name = "Count")]
        public int Count { get; set; }
    }
}