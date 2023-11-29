using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.DataAccess.Concrete
{
    public class MesajDal : RepositoryBase<Mesaj>, IMesajDal
    { 
        public MesajDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
