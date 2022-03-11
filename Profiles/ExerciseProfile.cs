using AutoMapper;
using models = MeFit.Models.Domain;
using MeFit.Models.DTOs.Exercise;

namespace MeFit.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<models.Exercise, ExerciseDTO>().ReverseMap();
        }
    }
}
