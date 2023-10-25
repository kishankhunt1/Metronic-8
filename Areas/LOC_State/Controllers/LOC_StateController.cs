using Microsoft.AspNetCore.Mvc;

namespace Metronic_8.Areas.LOC_State.Controllers
{
	[Area("LOC_State")]
	[Route("[Controller]/[action]")]
	public class LOC_StateController : Controller
	{
		public IActionResult Index()
		{
			return View("StateList");
		}
	}
}
