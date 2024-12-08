namespace Augury.MachineInfoService.Models.Domain
{
    public class ReadMachineModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Type { get; set; } 
        public int TotalComponents { get; set; }
    }
}
