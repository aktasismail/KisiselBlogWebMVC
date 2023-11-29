using ismailaktasblog.Entities;

namespace ismailaktasblog.Services.Abstract
{
    public interface IMakaleService
    {
        IEnumerable<Makale> Listele(bool trackchages);
        Task<MakaleDto> Getir(int id, bool trackchannges);
        Task<MakaleDto> Getiru(int id,string username, bool trackchannges);
        Task Ekle(MakaleDto makaleDto);
        Task Guncelle(MakaleDto makaleDto);
        Task Sil(int id);
    }
}
