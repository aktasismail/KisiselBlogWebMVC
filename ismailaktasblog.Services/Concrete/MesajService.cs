using AutoMapper;
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
    public class MesajService :IMesajService
    {
        private readonly IMesajDal _mesajDal;
        private readonly IMapper _mapper;
        public MesajService(IMesajDal mesajDal, IMapper mapper)
        {
            _mesajDal = mesajDal;
            _mapper = mapper;
        }

        public async Task MesajYaz(MesajDto mesajDto)
        {
            if (mesajDto == null)
            {
                throw new ArgumentNullException();
            }
            Mesaj mesaj = _mapper.Map<Mesaj>(mesajDto);
            await _mesajDal.Add(mesaj);
            await _mesajDal.SaveAsync();
        }
    }
}
