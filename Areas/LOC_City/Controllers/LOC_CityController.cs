using Microsoft.AspNetCore.Mvc;

namespace Metronic_8.Areas.LOC_City.Controllers
{
	[Area("LOC_City")]
	[Route("[Controller]/[action]")]
	public class LOC_CityController : Controller
	{
		public IActionResult Index()
		{
			return View("CityList");
		}
	}
}
