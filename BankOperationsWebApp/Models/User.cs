using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BankOperationsWebApp.Models
{
    public class User:IdentityUser
    {
        [ForeignKey("Card")]
        public int CardId { get; set; }

        public Card Card { get; set; }

        [ProtectedPersonalData]
        public string IbanNumber { get; set; }
    }
}