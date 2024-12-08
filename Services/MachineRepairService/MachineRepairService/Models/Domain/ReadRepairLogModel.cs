namespace Augury.RepairTelemetryService.Models.Domain
{
    public class ReadRepairLogModel
    {
        public string Id { get; set; } 
        public string UserName { get; set; } 
        public string ComponentName { get; set; }  
        public string Repair { get; set; }
        public string Summary { get; set; }
        public DateTime RepairDate { get; set; }
        public string RepairStatus { get; set; }  
        public string WorkOrderId { get; set; }
        public string MachineId { get; set; }
        public string MachineName { get; set; } 
        public string ReviewStatus { get; set; } 
        public string AreaType { get; set; } 
    }
}
