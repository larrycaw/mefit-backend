using AutoMapper;
using MeFit.Models;
using models = MeFit.Models.Domain;
using System.Linq;
using MeFit.Models.DTOs.Program;
using MeFit.Models.DTOs;

namespace MeFit.Profiles
{
    public class MFProgramProfile : Profile
    {
       public MFProgramProfile()
        {
            CreateMap<models.MFProgram, ProgramCreateDTO>().ReverseMap();
            CreateMap<models.MFProgram, ProgramEditDTO>().ReverseMap();
            CreateMap<models.MFProgram, ProgramReadDTO>()
                .ForMember(mdto => mdto.Workouts, opt =>
                opt.MapFrom(m => m.Workouts.Select(c => c.Id).ToArray()))
                .ReverseMap();
        }
    }
}
