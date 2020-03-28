using System;
using System.Net.Mail;
using System.Threading.Tasks;
using BankOperationsWebApp.Data;
using BankOperationsWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankOperationsWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;
        private readonly BankOptions _bank;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context, BankOptions bank)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _bank = bank;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, PhoneNumber = model.PhoneNumber, IbanNumber = _bank.GenerateIbanNumber(model.CardNumber)};
                Random random = new Random();
                Card card = new Card { Count = 5000, CardNumber = model.CardNumber, PinCode = random.Next(0, 9999).ToString().PadLeft(4, '0') }; 
                user.Card = card;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, false);

                    MailMessage mail = new MailMessage();
                    mail.To.Add(user.Email);
                    mail.From = new MailAddress("anastasiabaklayeva@gmail.com", "Bank E-transactions", System.Text.Encoding.UTF8);
                    mail.Subject = "PIN";
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = card.PinCode;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("anastasiabaklayeva@gmail.com", "Aa12345!");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        Exception ex2 = ex;
                        string errorMessage = string.Empty;
                        while (ex2 != null)
                        {
                            errorMessage += ex2.ToString();
                            ex2 = ex2.InnerException;
                        }
                    }
             
                return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel {ReturnUrl = returnUrl});
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and (or) password");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}