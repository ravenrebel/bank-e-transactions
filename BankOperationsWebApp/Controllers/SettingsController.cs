using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankOperationsWebApp.Data;
using BankOperationsWebApp.Models;
using BankOperationsWebApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankOperationsWebApp.Views
{
    public class SettingsController : Controller
    {
        private readonly ApplicationContext _context;

        public SettingsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePinCode()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePinCode(ChangePinCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (user != null)
                {
                    Card card = await _context.Cards.FirstOrDefaultAsync(c => c.Id.Equals(user.CardId));
                    if (card != null)
                    {
                        if (model.PinCode.Equals(card.PinCode))
                        {
                            card.PinCode = model.NewPinCode;
                            _context.Cards.Update(card);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", "Home");
                        }
                        else ViewBag.InCorrectPinCode = "Incorrect PIN";
                    }
                }
            }
            return View(model);
        }
    }
}