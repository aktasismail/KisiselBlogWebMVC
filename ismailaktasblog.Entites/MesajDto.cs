using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Entities
{
    public record MesajDto
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "Lülten adınızı giriniz")]
        public string Ad { get; init; }
        [Required(ErrorMessage = "Lülten e posta giriniz")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Lülten mesajınızı giriniz")]
        public string Mesajlar { get; init; }
    }
}
