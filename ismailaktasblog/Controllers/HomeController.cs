using ismailaktasblog.Entities;
using ismailaktasblog.Models;
using ismailaktasblog.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ismailaktasblog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMakaleService _makaleservice;
        private readonly IMesajService _mesajservice;
        public HomeController(ILogger<HomeController> logger, IMakaleService makaleservice, IMesajService mesajservice)
        {
            _logger = logger;
            _makaleservice = makaleservice;
            _mesajservice = mesajservice;
        }
        public IActionResult Index()
        {
            var makale = _makaleservice.Listele(false)
                .OrderByDescending(x=>x.MakaleId).Take(3);
            return View(makale);
        }
        public IActionResult Makale()
        {
            var makaleler = _makaleservice.Listele(false);
            return View(makaleler);
        }
        public async Task<IActionResult> Detay(int id)
        {
            var makale = await _makaleservice.Getir(id, false);
            return View(makale);
        }
        public IActionResult KategoriDetay(int? id)
        {
            var makale = _makaleservice.Listele(false).Where(i => i.KategoriId == id).ToList();
            return View(makale);
        }
        public IActionResult iletisim()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> iletisim(MesajDto mesajDto)
        {
            if (ModelState.IsValid)
            {
                await _mesajservice.MesajYaz(mesajDto);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", $"Lütfen tüm alaları doldurunuz!");
            return View();
        }
        public IActionResult Arama(string x)
        {
            if (!String.IsNullOrEmpty(x))
            {
                var ara = _makaleservice.Listele(false).Where(i => i.Baslik.Contains(x) || i.Detay.Contains(x));
                return View(ara);
            }
            return RedirectToAction(nameof(Error));
        }
        public IActionResult Hakkimda()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CV()
        {
            return View();
        }
    }
}
