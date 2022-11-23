using AutoMapper;
using SportsHub.Api.Mapping.Models;
using SportsHub.Domain.Models;

namespace SportsHub.Api.Mapping
{
    public class SportMapping : Profile
    {
        public SportMapping()
        {
            CreateMap<Sport, SportResponseDTO>()
                .ReverseMap();
        }
    }
}
