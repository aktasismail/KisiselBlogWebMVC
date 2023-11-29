using ismailaktasblog.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ismailaktasblog.ViewComponents
{
    public class KategoriVC : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public KategoriVC(ApplicationDbContext db)
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
