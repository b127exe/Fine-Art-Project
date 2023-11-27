using FineArt.Data;
using Microsoft.AspNetCore.Mvc;

namespace FineArt.Controllers
{
	public class WebController : Controller
	{
		private readonly FineArtDbContext _context;
        public WebController(FineArtDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
	}
}
