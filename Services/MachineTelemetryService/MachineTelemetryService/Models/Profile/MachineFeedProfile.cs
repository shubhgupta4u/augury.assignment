using Augury.RepairTelemetryService.Models.Domain;
using Augury.RepairTelemetryService.Models.Entity;

namespace Augury.RepairTelemetryService.Models.Profile
{
    public class MachineFeedProfile : AutoMapper.Profile
    {
        public MachineFeedProfile()
        {
            CreateMap<MachineFeed, ReadMachineFeedModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))  
                .ForMember(dest => dest.ComponentCounts, opt => opt.MapFrom(src => src.Components.Count))  
                .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.MachineId)) 
                .ForMember(dest => dest.EnableInterim, opt => opt.MapFrom(src => src.EnableInterim))
                .ForMember(dest => dest.Continuous, opt => opt.MapFrom(src => src.Continuous))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EnableInterim, opt => opt.MapFrom(src => src.EnableInterim))  
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.StatusUpdatedAt, opt => opt.MapFrom(src => src.StatusUpdatedAt))  
                .ForMember(dest => dest.TagCount, opt => opt.MapFrom(src => src.Tags.Count))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt)); 
        }
    }
}
