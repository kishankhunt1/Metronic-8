using Microsoft.AspNetCore.Mvc;

namespace Metronic_8.Areas.LOC_Country.Controllers
{
	[Area("LOC_Country")]
	[Route("[Controller]/[action]")]
	public class LOC_CountryController : Controller
	{
		public IActionResult Index()
		{
			return View("CountryList");
		}
		public IActionResult Add()
		{
			return View("CountryAddEdit");
		}
	}
}
