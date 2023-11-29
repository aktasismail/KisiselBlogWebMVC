using ismailaktasblog.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ismailaktasblog.Models;

namespace ismailaktasblog.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private IMailSender _emailSender;
        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            IMailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(GirisYapmaVM girisYapmaVM)
        {
            if (ModelState.IsValid)
            {
                var entity = await _userManager.FindByEmailAsync(girisYapmaVM.Email);
                if (entity != null)
                {
                    await _signInManager.SignOutAsync();
                    var sonuc = await _signInManager.PasswordSignInAsync(entity, girisYapmaVM.Password, girisYapmaVM.BeniHatirla, true);
                    if (sonuc.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(entity);
                        await _userManager.SetLockoutEndDateAsync(entity, null);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (sonuc.IsLockedOut)
                    {
                        var kacdakika = await _userManager.GetLockoutEndDateAsync(entity);
                        var bloekesayacı = kacdakika.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Hesabınız kitlendi, Lütfen {kacdakika.Value} dakika sonra deneyiniz");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı");
                }

                return View(girisYapmaVM);
            }
            ModelState.AddModelError("", $"Lütfen tüm alanları dolddurunuz");
            return View();
        }
        public IActionResult KayitOlma()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOlma(KayitOlmaVM kayitOlmaVM)
        {
            if (ModelState.IsValid)
            {
                var yeni = new AppUser
                {
                    Ad = kayitOlmaVM.Ad,
                    Soyad = kayitOlmaVM.Soyad,
                    UserName = kayitOlmaVM.UserName,
                    Email = kayitOlmaVM.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(yeni, kayitOlmaVM.Password);
                if (result.Succeeded)
                {
                    var addrole = await _userManager.AddToRoleAsync(yeni, "Member");

                    if (addrole.Succeeded)
                    {
                        return RedirectToAction("GirisYap", "Account");
                    }
                }
                foreach (IdentityError hata in result.Errors)
                {
                    ModelState.AddModelError("", hata.Description);
                }
            }
            return View(kayitOlmaVM);
        }
        public async Task<IActionResult> CikisYap([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }
        public async Task<IActionResult> MailGonder(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("EmailDogrula", "Account", new { user.Id, token });
            await _emailSender.SendEmailAsync(Email, "Parola Sıfırlama", $"Hesabınızı doğrulamak için tıklayınız <a href='https://ismailaktas.net{url}'>tıklayınız.</a>.");
            TempData["message"] = "Lütfen e postanızı kontrol ediniz.";

            return RedirectToAction("Profilim", "Home", new { area = "Users" });
        }
        public async Task<IActionResult> EmailDogrula(string Id, string token)
        {
            if (Id == null || token == null)
            {
                TempData["message"] = "Geçersiz token bilgisi";
                return View();
            }
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı";
                    return RedirectToAction("Profilim", "Home", new { area = "Users" });
                }
            }
            TempData["message"] = "Kullanıcı bulunamadı";
            return View();
        }
        public IActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SifremiUnuttum(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData["message"] = "Eposta adresinizi giriniz.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                TempData["message"] = "Eposta adresiyle eşleşen bir kayıt yok.";
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("SifreSifirla", "Account", new { user.Id, token });

            await _emailSender.SendEmailAsync(Email, "Parola Sıfırlama", $"Şİfrenizi yenilemek için linke <a href='http://ismailaktas.net{url}'>tıklayınız.</a>.");

            TempData["message"] = "Eposta adresinize gönderilen link ile şifrenizi sıfırlayabilirsiniz.";

            return View();

        }

        public IActionResult SifreSifirla(string Id, string token)
        {
            if (Id == null || token == null)
            {
                return RedirectToAction("GirisYap");
            }

            var model = new SifreSifirlaVM { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SifreSifirla(SifreSifirlaVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["message"] = "Bu mail adresiyle eşleşen kullanıcı yok.";
                return RedirectToAction("GirisYap");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData["message"] = "Şifreniz değiştirildi";
                return RedirectToAction("GirisYap");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            return View(model);
        }
    }
}
