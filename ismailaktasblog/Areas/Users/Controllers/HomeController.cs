using ismailaktasblog.Entities;
using ismailaktasblog.Models;
using ismailaktasblog.Services.Abstract;
using ismailaktasblog.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ismailaktasblog.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMakaleService _makaleservice;
        private readonly IKategoriService _kategoriservice;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(IMakaleService makaleservice, UserManager<AppUser> userManager, IKategoriService kategoriservice)
        {
            _makaleservice = makaleservice;
            _userManager = userManager;
            _kategoriservice = kategoriservice;
        }
        public async Task<IActionResult> Profilim()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = new ProfilVM
            {
                AppUser = await _userManager.GetUserAsync(this.User),
                Makale = _makaleservice.Listele(false).Where(x => x.UserId == userId)
            };
            return View(profile);
        }
        public async Task<IActionResult> ProfilDuzenle(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(new KullanıcıDuzenleVM
                {
                    Id = user.Id,
                    Ad = user.Ad,
                    Soyad = user.Soyad,
                    ProfilResmi = user.ProfilResmi,
                    UserName = user.UserName,
                    Email = user.Email,
                });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ProfilDuzenle([FromForm] string id, KullanıcıDuzenleVM model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", file.FileName);
                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}
                //user.ProfilResmi = String.Concat("~/css/", file.FileName);
                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }
            return View(model);
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
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", formFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            makale.Gorsel = String.Concat("/img/", formFile.FileName);
            makale.UserId = userId;
            await _makaleservice.Ekle(makale);
            return RedirectToAction("MakaleOnay");
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
            var userIdx = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdx == userId)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", formFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                makaleDto.Gorsel = String.Concat("/img/", formFile.FileName);
                makaleDto.UserId = userIdx;
                makaleDto.Onay = false;
                await _makaleservice.Guncelle(makaleDto);
                return RedirectToAction("MakaleOnay");
            }
            return Forbid();

        }
        public async Task<IActionResult> MakaleSil([FromRoute] int id, string userid)
        {
            var userIdx = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdx == userid)
            {
                await _makaleservice.Sil(id);
                TempData[""] = "Silindi.";
                return RedirectToAction("Profilim");
            }
            return Forbid();
        }
        public IActionResult MakaleOnay()
        {
            return View();
        }
    }
}
