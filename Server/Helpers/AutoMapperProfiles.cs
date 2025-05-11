using BlazorPeliculas.Shared.Entidades;
using AutoMapper;

namespace BlazorPeliculas.Server.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<Actor,Actor>()
                 .ForMember(x => x.Foto, options => options.Ignore());

            CreateMap<Pelicula, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore());
                
        }
    }
   
}
