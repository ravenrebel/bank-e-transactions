using System.Threading.Tasks;
using BankOperationsWebApp.Data;
using BankOperationsWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankOperationsWebApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationContext _context;

        public TransactionsController(ApplicationContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult CardNumberTransfer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IbanTransfer()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CardNumberTransfer(CardNumberTransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (user != null)
                {
                    Card receiverCard =  await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber.Equals(model.CardNumber));
                    Card userCard =  await _context.Cards.FirstOrDefaultAsync(c => c.Id.Equals(user.CardId));
                    if (receiverCard != null && userCard.Count >= model.Count)
                    {
                        userCard.Count = userCard.Count  - model.Count;
                        receiverCard.Count = receiverCard.Count + model.Count;
                        _context.Cards.Update(receiverCard);
                        _context.Cards.Update(userCard);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> IbanTransfer(IbanTransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                User receiver = await _context.Users.FirstOrDefaultAsync(u => u.IbanNumber.Equals(model.IbanNumber));
                if (user != null && receiver != null)
                {
                    Card receiverCard = await _context.Cards.FirstOrDefaultAsync(c => c.Id.Equals(receiver.CardId));
                    Card userCard = await _context.Cards.FirstOrDefaultAsync(c => c.Id.Equals(user.CardId));
                    if (userCard.Count >= model.Count)
                    {
                        userCard.Count = userCard.Count - model.Count;
                        receiverCard.Count = receiverCard.Count + model.Count;
                        _context.Cards.Update(receiverCard);
                        _context.Cards.Update(userCard);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

       
   
        //UserDTO with card props
    }
}