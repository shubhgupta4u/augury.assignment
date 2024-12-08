namespace Augury.RepairTelemetryService.Models.Domain
{
    public class ReadMachineFeedModel
    {
        public string Id { get; set; }
        public string MachineId { get; set; }
        public string Status { get; set; }
        public DateTime StatusUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Continuous { get; set; }
        public bool EnableInterim { get; set; }
        public int TagCount { get; set; }
        public int ComponentCounts { get; set; }
    }
}
