namespace Augury.Gateway.Models
{
    public class MachineFeed
    {
        public string MachineId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Type { get; set; }
        public int TotalComponents { get; set; }
        public string FeedId { get; set; }
        public string FeedType { get; set; }
        public DateTime FeedDate { get; set; }
        public IDictionary<string, string> FeedMetaData { get; set; }
    }
}
