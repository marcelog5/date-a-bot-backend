using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class OutputDetail
    {
        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("artifacts")]
        public Artifacts Artifacts { get; set; }

        [JsonProperty("outputs")]
        public OutputMessage Outputs { get; set; }

        [JsonProperty("logs")]
        public Logs Logs { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("component_display_name")]
        public string ComponentDisplayName { get; set; }

        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("used_frozen_result")]
        public bool UsedFrozenResult { get; set; }
    }
}
