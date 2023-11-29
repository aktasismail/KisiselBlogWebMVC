using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ismailaktasblog.Entities
{
    public class KayitOlmaVM
    {
        [Required]
        public string Ad { get; set; } = string.Empty;
        [Required]
        public string Soyad { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
