using kt13_10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.DanhSachCauThu = db.Cauthus.AsNoTracking().Where(x => x.CauLacBoId == "101").ToList();

            return View(lsttrandau);
        }

        [Route("CapNhatCauThu")]
        [HttpGet]
        public IActionResult CapNhatCauThu(string? idCauThu)
        {




            var cauThu = db.Cauthus.Where(c => c.CauThuId == idCauThu).FirstOrDefault();

            ViewBag.CauLacBoId = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            return View(cauThu);

        }

        [Route("CapNhatCauThu")]
        [HttpPost]
        public IActionResult CapNhatCauThu(Cauthu cauthu)
        {


            if (ModelState.IsValid)
            {   //cach1
                db.Update(cauthu);
                //cach2  db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.CauLacBoId = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");

            return View(cauthu);


        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
