using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ismailaktasblog.Entities
{
    public class Yorumlar
    {
        [Key]
        public int YorumId { get; set; }
        public string? AdSoyad { get; set; }
        public string? Yorum { get; set; }
        public int MakaleId { get; set; }
        [ForeignKey("MakaleId")]
        public Makale Makale { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; } = null!;
    }
}
