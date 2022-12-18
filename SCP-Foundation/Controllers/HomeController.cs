using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCP_Foundation.Models;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SCP_Foundation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        //private SCPcontext context { get; set; }
        //public HomeController(SCPcontext ctx)
        //{
        //    context = ctx;
        //}
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Database()
        {
            //var scps = context.SCPs
            //    .Include(s => s.Classified)
            //    .Include(s => s.Risk)
            //    .Include(s => s.Disruption)
            //    .Include(s => s.Contain)
            //    .OrderBy(s => s.IdNumber).ToList();
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
