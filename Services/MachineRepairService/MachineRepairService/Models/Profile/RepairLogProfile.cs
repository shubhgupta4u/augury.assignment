using Augury.RepairTelemetryService.Models.Domain;
using Augury.RepairTelemetryService.Models.Entity;

namespace Augury.RepairTelemetryService.Models.Profile
{
    public class RepairLogProfile : AutoMapper.Profile
    {
        public RepairLogProfile()
        {
            CreateMap<RepairLog, ReadRepairLogModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))  
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))  
                .ForMember(dest => dest.ComponentName, opt => opt.MapFrom(src => src.Machine.Name)) 
                .ForMember(dest => dest.Repair, opt => opt.MapFrom(src => src.Repair))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.RepairDate, opt => opt.MapFrom(src => src.RepairDate))
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => src.RepairWorkflowStatus.RepairStatus))  
                .ForMember(dest => dest.WorkOrderId, opt => opt.MapFrom(src => src.WorkOrderId))
                .ForMember(dest => dest.MachineId, opt => opt.MapFrom(src => src.Machine.Id))
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine.Name))  
                .ForMember(dest => dest.ReviewStatus, opt => opt.MapFrom(src => src.ReviewStatus))
                .ForMember(dest => dest.AreaType, opt => opt.MapFrom(src => src.FixAreaDetails.AreaType)); 
        }
    }
}
