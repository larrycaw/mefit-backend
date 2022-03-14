using AutoMapper;
using MeFit.Models;
using models = MeFit.Models.Domain;
using System.Linq;
using MeFit.Models.DTOs.Set;
using MeFit.Models.DTOs;


namespace MeFit.Profiles
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<models.Set, SetCreateDTO>().ReverseMap();
            CreateMap<models.Set, SetEditDTO>().ReverseMap();
            CreateMap<models.Set, SetReadDTO>()
                .ForMember(mdto => mdto.Workouts, opt =>
                opt.MapFrom(m => m.Workouts.Select(c => c.Id).ToArray()))
                .ReverseMap();
        }
    }
}
