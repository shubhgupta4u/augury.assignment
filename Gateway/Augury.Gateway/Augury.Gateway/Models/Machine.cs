namespace Augury.Gateway.Models
{
    using System.Linq;

    public class Machine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Type { get; set; } 
        public int TotalComponents { get; set; }
        public int TotalRepairCount => RepairFeeds?.Count() ?? 0;
        public int TotalSessionCount => SessionFeeds?.Count() ?? 0;
        public SessionFeed[] SessionFeeds { get; set; }
        public RepairFeed[] RepairFeeds { get; set; }
    }
}
