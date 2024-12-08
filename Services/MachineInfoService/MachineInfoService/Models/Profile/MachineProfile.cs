using Augury.MachineInfoService.Models.Domain;
using Augury.MachineInfoService.Models.Entity;

namespace Augury.MachineInfoService.Models.Profile
{
    public class MachineProfile : AutoMapper.Profile
    {
        public MachineProfile()
        {
            CreateMap<Machine, ReadMachineModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TechnicalSpecs.Type))
                .ForMember(dest => dest.TotalComponents, opt => opt.MapFrom(src => src.Components != null ? src.Components.Count : 0));
        }
    }
}
