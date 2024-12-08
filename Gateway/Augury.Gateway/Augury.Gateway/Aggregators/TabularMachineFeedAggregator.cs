namespace Augury.Gateway.Aggregators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.Json;
    using Augury.Gateway.Models;
    using Ocelot.Middleware;
    using Ocelot.Multiplexer;

    public class TabularMachineFeedAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            try
            {
                var machineResponse = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
                var repairFeedResponse = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
                var sessionFeedResponse = await responses[2].Items.DownstreamResponse().Content.ReadAsStringAsync();

                // Deserialize responses into models
                var machineData = JsonSerializer.Deserialize<Machine>(machineResponse, SerializerOptions);
                var repairFeedData = JsonSerializer.Deserialize<RepairFeed[]>(repairFeedResponse, SerializerOptions);
                var sessionFeedData = JsonSerializer.Deserialize<SessionFeed[]>(sessionFeedResponse, SerializerOptions);

                // Merge the data
                if (responses != null && machineData != null)
                {
                    machineData.RepairFeeds = repairFeedData;
                    machineData.SessionFeeds = sessionFeedData;
                }
                var machineFeeds = MapMachineToFeed(machineData);
                var jsonResult = JsonSerializer.Serialize(machineFeeds);
                var content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

                return new DownstreamResponse(content, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "application/json");
            }
            catch (Exception ex)
            {
                // Return a detailed error message
                var errorMessage = new
                {
                    Message = "An error occurred during aggregation.",
                    Details = ex.Message,
                    StackTrace = ex.StackTrace // Optionally include stack trace for debugging
                };

                // Serialize the error object to JSON
                var jsonError = JsonSerializer.Serialize(errorMessage);

                return new DownstreamResponse(
                    new StringContent(jsonError, Encoding.UTF8, "application/json"),
                    HttpStatusCode.InternalServerError,
                    new List<Header>(),
                    "Internal Server Error");
            }
        } 

        private JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true 
        };

        private List<MachineFeed> MapMachineToFeed(Machine machine)
        {
            List<MachineFeed> feeds = new List < MachineFeed >();
            if (machine != null)
            {
                if(machine.RepairFeeds != null && machine.RepairFeeds.Count() > 0)
                {
                    feeds.AddRange(machine.RepairFeeds.Select(s => new MachineFeed
                    {
                        MachineId = machine.Id,
                        Name = machine.Name,
                        CreatedAt = machine.CreatedAt,
                        UpdatedAt = machine.UpdatedAt,
                        UpdatedBy = machine.UpdatedBy,
                        Type = machine.Type,
                        TotalComponents = machine.TotalComponents,
                        FeedId = s.Id,
                        FeedType="Repair",
                        FeedDate = s.RepairDate,
                        FeedMetaData = new Dictionary<string, string>
                        {
                            { "ComponentName", s.ComponentName },
                            { "RepairDate", s.RepairDate.ToString("yyyy-MM-ddTHH:mm:ssZ") },
                            { "RepairStatus", s.RepairStatus }
                        }
                    }));
                }
                if (machine.SessionFeeds != null && machine.SessionFeeds.Count() > 0)
                {
                    feeds.AddRange(machine.SessionFeeds.Select(s => new MachineFeed
                    {
                        MachineId = machine.Id,
                        Name = machine.Name,
                        CreatedAt = machine.CreatedAt,
                        UpdatedAt = machine.UpdatedAt,
                        UpdatedBy = machine.UpdatedBy,
                        Type = machine.Type,
                        TotalComponents = machine.TotalComponents,
                        FeedId = s.Id,
                        FeedType = "Session",
                        FeedDate = s.CreatedAt,
                        FeedMetaData = new Dictionary<string, string>
                        {
                            { "Status", s.Status },
                            { "StatusUpdatedAt", s.StatusUpdatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ") },
                            { "ComponentCounts", s.ComponentCounts.ToString() }
                        }
                    }));
                }
            }
            return feeds.OrderByDescending(s=>s.FeedDate).ToList();
        }
    }

}
