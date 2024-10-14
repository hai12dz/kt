using kt13_10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace kt13_10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        QlgiaiBongDaContext db = new QlgiaiBongDaContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
		{
			var lsttrandau = db.Trandaus.Take(7).ToList();
			ViewBag.DanhSachCauThu = db.Cauthus.AsNoTracking().Where(x => x.CauLacBoId =="101").ToList();

			// Kiểm tra số lượng cầu thủ
			var count = ViewBag.DanhSachCauThu.Count;

			if (count == 0)
			{
				Console.WriteLine("Không có cầu thủ nào trong ViewBag");
			}

			return View(lsttrandau);
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
