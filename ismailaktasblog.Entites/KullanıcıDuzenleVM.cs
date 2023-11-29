
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Entities
{
    public class KullanıcıDuzenleVM
    {
        public string? Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? ProfilResmi { get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set;}

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil")]
        public string? ConfirmPassword { get; set; }
    }
}
