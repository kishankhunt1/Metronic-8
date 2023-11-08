using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.BAL;
using Metronic_8.DAL;
using Metronic_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Diagnostics;

namespace Metronic_8.Controllers
{
	[CheckAccess]
	public class HomeController : Controller
    {
        LOC_DAL dal=new LOC_DAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}