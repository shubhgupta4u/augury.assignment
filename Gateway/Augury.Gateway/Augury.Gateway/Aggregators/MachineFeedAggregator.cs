namespace Augury.Gateway.Aggregators
{
    using System.Text.Json;
    using Augury.Gateway.Models;
    using Ocelot.Middleware;
    using Ocelot.Multiplexer;

    public class MachineFeedAggregator : IDefinedAggregator
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
                if(responses !=null && machineData != null)
                {
                    machineData.RepairFeeds = repairFeedData;
                    machineData.SessionFeeds = sessionFeedData;
                }

                var jsonResult = JsonSerializer.Serialize(machineData);
                var content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");

                return new DownstreamResponse(content, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "application/json");
            }catch(Exception ex)
            {
                var jsonResult = JsonSerializer.Serialize(ex);
                var content = new StringContent(jsonResult, System.Text.Encoding.UTF8, "application/json");
                return new DownstreamResponse(content, System.Net.HttpStatusCode.InternalServerError, new List<KeyValuePair<string, IEnumerable<string>>>(), "application/json");
            }            
        }

        private JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true 
        };
    }

}
