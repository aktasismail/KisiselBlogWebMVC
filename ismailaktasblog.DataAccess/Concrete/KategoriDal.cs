using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.DataAccess.Concrete
{
    public class KategoriDal : RepositoryBase<Kategori>, IKategoriDal
    {
        public KategoriDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
