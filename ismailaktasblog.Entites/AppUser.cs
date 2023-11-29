using Microsoft.AspNetCore.Identity;
namespace ismailaktasblog.Entities
{
    public class AppUser:IdentityUser
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? ProfilResmi { get; set; }
        public List<Makale> Makale { get; set; } = new List<Makale>();
        public List<Yorumlar> Yorumlar { get; set; }
    }
}
