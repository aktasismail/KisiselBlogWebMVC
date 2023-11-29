using ismailaktasblog.Entities;

namespace ismailaktasblog.Models
{
    public class ProfilVM
    {
        public IEnumerable<Makale> Makale { get; set; }
        public AppUser AppUser { get; set; }
    }
}
