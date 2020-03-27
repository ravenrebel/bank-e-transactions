using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BankOperationsWebApp.Models
{
    public class Card
    {
        public int Id { get; set; }

        [ProtectedPersonalData]
        public  string PinCode { get; set; }
        
        [ProtectedPersonalData]
        public string CardNumber { get; set; }
        
        [ProtectedPersonalData]
        public int Count { get; set; }
    }
}