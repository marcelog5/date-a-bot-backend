using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class OutputMessage
    {
        [JsonProperty("message")]
        public MessageDetail Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
