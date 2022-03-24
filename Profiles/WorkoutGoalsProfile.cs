using AutoMapper;
using models = MeFit.Models.Domain;
using MeFit.Models.DTOs.WorkoutGoals;
namespace MeFit.Profiles
{
    public class WorkoutGoalsProfile : Profile
    {
            public WorkoutGoalsProfile()
            {
                CreateMap<models.GoalWorkouts, WorkoutGoalsReadDTO>().ReverseMap();
                CreateMap<models.GoalWorkouts, WorkoutGoalsCreateDTO>().ReverseMap();
                CreateMap<models.GoalWorkouts, WorkoutGoalsEditDTO>().ReverseMap();

        }
    }
}
