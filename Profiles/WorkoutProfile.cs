using AutoMapper;
using MeFit.Models;
using models = MeFit.Models.Domain;
using System.Linq;
using MeFit.Models.DTOs.Workout;
using MeFit.Models.DTOs;

namespace MeFit.Profiles
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<models.Workout, WorkoutCreateDTO>().ReverseMap();
            CreateMap<models.Workout, WorkoutEditDTO>().ReverseMap();
            CreateMap<models.Workout, WorkoutReadDTO>()
                .ForMember(mdto => mdto.Sets, opt =>
                opt.MapFrom(m => m.Sets.Select(c => c.Id).ToArray()))
                .ForMember(mdto => mdto.Programs, opt =>
                opt.MapFrom(m => m.Programs.Select(c => c.Id).ToArray()))
                .ReverseMap();
        }
    }
}
