using AutoMapper;
using MeFit.Models.DTOs.Goal;
using System.Linq;
using models = MeFit.Models.Domain;

namespace MeFit.Profiles
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<models.Goal, GoalReadDTO>()
                .ForMember(gdto => gdto.Workouts, opt =>
                opt.MapFrom(g => g.Workouts.Select(w => w.Id).ToArray()))
                .ReverseMap();

            CreateMap<models.Goal, GoalByUserDTO>()
                .ForMember(gdto => gdto.Workouts, opt =>
                opt.MapFrom(g => g.Workouts.Select(w => w.Id).ToArray()))
                .ReverseMap();

            CreateMap<models.Goal, GoalCreateDTO>().ReverseMap();
        }
    }
}
