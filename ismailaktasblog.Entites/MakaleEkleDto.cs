using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Entities
{
    public class MakaleEkleDto
    {
        public int MakaleId { get; set; }
        public string? Baslik { get; set; }
        public string? Detay { get; set; }
        public string? Gorsel { get; set; }
        public DateTime Tarih { get; set; }
        public string UserId { get; set; }
        public int KategoriId { get; set; }
    }
}
