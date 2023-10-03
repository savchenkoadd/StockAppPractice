using Microsoft.AspNetCore.Mvc;

namespace Config.Controllers
{
	public class HomeController : Controller
	{
		private readonly IConfiguration _configuration;

		[Route("/")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
