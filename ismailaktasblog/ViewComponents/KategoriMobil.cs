using ismailaktasblog.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ismailaktasblog.ViewComponents
{

    public class KategoriMobil : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public KategoriMobil(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var kategori = _db.Kategori.ToList();
            return View(kategori);
        }
    }
}
