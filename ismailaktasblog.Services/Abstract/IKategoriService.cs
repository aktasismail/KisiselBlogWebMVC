using ismailaktasblog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Services.Abstract
{
    public interface IKategoriService
    {
        IEnumerable<Kategori> Listele(bool trackchages);
    }
}
