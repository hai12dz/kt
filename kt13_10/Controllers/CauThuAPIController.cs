using kt13_10.Models;
using kt13_10.Models.CauThuModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace kt13_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauThuAPIController : ControllerBase
    {
        QlgiaiBongDaContext db = new QlgiaiBongDaContext();

        [HttpGet("{matrandau}")]
        public IEnumerable<CauThuTheoMa> GetCauThuTheoMa(string matrandau)
        {
            var lstMaCauThuTranDau = db.TrandauCauthus.AsNoTracking()
                .Where(x => x.TranDauId == matrandau)
                .Select(x => x.CauThuId)
                .ToList();

            var cauThus = (from p in db.Cauthus
                           where lstMaCauThuTranDau.Contains(p.CauThuId)
                           select new CauThuTheoMa
                           {
                               CauThuId = p.CauThuId,
                               HoVaTen = p.HoVaTen,
                               QuocTich = p.QuocTich,
                               Anh = p.Anh,
                               Ngaysinh = p.Ngaysinh,
                           }).ToList();
            if (cauThus.Count != 0) {
                Console.WriteLine("jhello");
            }
            return cauThus;
        }
    }
}

