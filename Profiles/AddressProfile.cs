using AutoMapper;
using models = MeFit.Models.Domain;
using MeFit.Models.DTOs.Address;

namespace MeFit.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<models.Address, AddressReadDTO>().ReverseMap();
            CreateMap<models.Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<models.Address, AddressCreateDTO>().ReverseMap();
        }
    }
}