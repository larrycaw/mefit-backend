using AutoMapper;
using models = MeFit.Models.Domain;
using MeFit.Models.DTOs.Exercise;

namespace MeFit.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<models.Exercise, ExerciseReadDTO>().ReverseMap();
            CreateMap<models.Exercise, ExerciseUpdateDTO>().ReverseMap();
            CreateMap<models.Exercise, ExerciseCreateDTO>().ReverseMap();
        }
    }
}
