using Bot.Http.Helpers;
using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Artifacts
    {
        [JsonProperty("message")]
        [JsonConverter(typeof(MessageDetailConverter))]
        public MessageDetail Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
