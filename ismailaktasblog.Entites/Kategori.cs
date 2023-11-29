using System.ComponentModel.DataAnnotations;

namespace ismailaktasblog.Entities
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required]
        public string? KategoriAd { get; set; }
        public List<Makale> Makale { get; set; } = new List<Makale>();
    }
}
