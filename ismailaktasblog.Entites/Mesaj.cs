using System.ComponentModel.DataAnnotations;

namespace ismailaktasblog.Entities
{
    public class Mesaj
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lülten adınızı giriniz")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Lülten e posta giriniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lülten mesajınızı giriniz")]
        public string Mesajlar { get; set; }
    }
}
