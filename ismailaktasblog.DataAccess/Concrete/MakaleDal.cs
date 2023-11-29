using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.DataAccess.Concrete
{
    public class MakaleDal : RepositoryBase<Makale>, IMakaleDal
    {
        public MakaleDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
