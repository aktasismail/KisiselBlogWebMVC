using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.Entities;
using ismailaktasblog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Services.Concrete
{
    public class KategoriService : IKategoriService
    {
        private readonly IKategoriDal _kategoriDal;
        public KategoriService(IKategoriDal kategoriDal)
        {
            _kategoriDal = kategoriDal;
        }
        public IEnumerable<Kategori> Listele(bool trackchages)
        {
            var kategori = _kategoriDal.GetAll(trackchages);
            return kategori;
        }
    }
}
