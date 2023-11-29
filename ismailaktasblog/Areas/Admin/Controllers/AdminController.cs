using ismailaktasblog.Entities;
using ismailaktasblog.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ismailaktasblog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMakaleService _makaleservice;
        private readonly IKategoriService _kategoriservice;
        public AdminController(IMakaleService makaleservice, IKategoriService kategoriservice)
        {
            _makaleservice = makaleservice;
            _kategoriservice = kategoriservice;
        }
        public IActionResult Index()
        {
            var makaleler = _makaleservice.Listele(false);
            return View(makaleler);
        }
        private SelectList SelectListMakale()
        {
            return new SelectList(_kategoriservice.Listele(false), "KategoriId", "KategoriAd", "2");
        }

        public IActionResult MakaleYaz()
        {
            ViewBag.kgrt = SelectListMakale();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakaleYaz([FromForm] MakaleDto makale, IFormFile formFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", formFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                makale.Gorsel = String.Concat("/img/", formFile.FileName);
                makale.UserId = userId;
                await _makaleservice.Ekle(makale);
                TempData["Ekledi"] = $"{makale.Baslik} Adlı makale eklendi.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> MakaleGuncelle(int id)
        {
            ViewBag.kgrt = SelectListMakale();
            var entity = await _makaleservice.Getir(id, false);
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> MakaleGuncelle([FromForm] MakaleDto makaleDto, string userId, IFormFile formFile)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", formFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            makaleDto.Gorsel = String.Concat("/img/", formFile.FileName);
            makaleDto.UserId = userId;
            await _makaleservice.Guncelle(makaleDto);
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> MakaleSil([FromRoute(Name = "id")] int id)
        {
            await _makaleservice.Sil(id);
            TempData[""] = "Silindi.";
            return RedirectToAction("Index");
        }
    }
}
