using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Output
    {
        [JsonProperty("inputs")]
        public Inputs Inputs { get; set; }

        [JsonProperty("outputs")]
        public List<OutputDetail> OutputsDetails { get; set; }
    }
}
