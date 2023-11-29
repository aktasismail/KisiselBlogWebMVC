
using AutoMapper;
using ismailaktasblog.DataAccess.Abstract;
using ismailaktasblog.DataAccess.Concrete;
using ismailaktasblog.Entities;
using ismailaktasblog.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Services.Concrete
{
    public class MakaleService : IMakaleService
    {
        private readonly IMakaleDal _makaleDal;
        private readonly IMapper _mapper;
        public MakaleService(IMakaleDal makaleDal, IMapper mapper)
        {
            _makaleDal = makaleDal;
            _mapper = mapper;
        }
        public async Task Ekle(MakaleDto makaleDto)
        {
            if (makaleDto == null)
            {
                throw new ArgumentNullException();
            }
            Makale makale = _mapper.Map<Makale>(makaleDto);
            makale.Tarih = DateTime.Now;
            await _makaleDal.Add(makale);
            await _makaleDal.SaveAsync();
        }
        public async Task Guncelle(MakaleDto makaleDto)
        {
            var makale = _mapper.Map<Makale>(makaleDto);
            makale.Tarih = DateTime.Now;
            _makaleDal.Update(makale);
            await _makaleDal.SaveAsync();
        }
        public async Task Sil(int id)
        {
           await _makaleDal.Delete(id);
           _makaleDal.Save();
        }
        public IEnumerable<Makale> Listele(bool trackchages)
        {
            var makale =  _makaleDal.GetAll(trackchages);
            return makale;
        }
        public async Task<MakaleDto> Getir(int id, bool trackchages)
        {
            var makale = await _makaleDal.Find(m=>m.MakaleId.Equals(id), trackchages);
            return _mapper.Map<MakaleDto>(makale);
        }

        public async Task<MakaleDto> Getiru(int id, string userId, bool trackchanges)
        {
            var makale = await _makaleDal.Find(m => m.MakaleId.Equals(id) && m.UserId.Equals(userId), trackchanges);
            return _mapper.Map<MakaleDto>(makale);
        }
    }
}
