using AutoMapper;
using models = MeFit.Models.Domain;
using MeFit.Models.DTOs.Profile;

namespace MeFit.Profiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<models.Profile, ProfileCreateDTO>().ReverseMap();
            CreateMap<models.Profile, ProfileReadDTO>().ReverseMap();
            CreateMap<models.Profile, ProfileUpdateDTO>().ReverseMap();
        }
    }
}