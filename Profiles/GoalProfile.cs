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
            CreateMap<models.Goal, GoalReadDTO>().ReverseMap();

            CreateMap<models.Goal, GoalByUserDTO>().ReverseMap();

            CreateMap<models.Goal, GoalCreateDTO>().ReverseMap();
            CreateMap<models.Goal, GoalEditDTO>().ReverseMap();
        }
    }
}
