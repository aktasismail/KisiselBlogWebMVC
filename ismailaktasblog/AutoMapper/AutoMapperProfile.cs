using AutoMapper;
using ismailaktasblog.Entities;

namespace ismailaktasblog.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MakaleDto, Makale>().ReverseMap();
            CreateMap<MesajDto, Mesaj>().ReverseMap();
        }
    }
}
