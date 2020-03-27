using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankOperationsWebApp.Models;
using BankOperationsWebApp.Data;
using AutoMapper;
using BankOperationsWebApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BankOperationsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger; 
            _context = context;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Index()
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user != null)
            {
                Card card = await _context.Cards.FirstOrDefaultAsync(c => c.Id.Equals(user.CardId));
                var config = new MapperConfiguration(c => c.CreateMap<Card, CardDTO>());
                IMapper mapper = config.CreateMapper();
                CardDTO cardDTO = mapper.Map<Card, CardDTO>(card);
                return View(cardDTO);
            }
            else return View(null);
        }
    }
}