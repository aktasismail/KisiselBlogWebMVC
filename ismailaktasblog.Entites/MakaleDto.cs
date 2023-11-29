using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Entities
{
    public record MakaleDto
    {
        public int MakaleId { get; init; }
        [Required(ErrorMessage = "Lülten başlık giriniz")]
        public string? Baslik { get; init; }
        [Required(ErrorMessage = "Lülten detay giriniz")]
        public string? Detay { get; init; }
        [Required(ErrorMessage = "Lülten görsel seçiniz")]
        public string? Gorsel { get; set; }
        public DateTime Tarih { get; init; }
        public bool Onay { get; set; } = false;
        public string UserId { get; set; }
        [Required(ErrorMessage = "Lülten kategori giriniz")]
        public int KategoriId { get; init; }
    }
}
