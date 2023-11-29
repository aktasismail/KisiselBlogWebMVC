using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ismailaktasblog.Entities
{
    public class Makale
    {
        public int MakaleId { get; set; }
        public string? Baslik { get; set; }
        public string? Detay { get; set; }
        public string? Gorsel { get; set; }
        public bool Onay { get; set; } = false;
        public DateTime Tarih {  get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; } = null!;
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori Kategori { get; set; }
        public List<Yorumlar> Yorumlar { get; set; } = new List<Yorumlar>();

    }
}
