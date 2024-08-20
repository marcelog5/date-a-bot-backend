using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Results
    {
        [JsonProperty("message")]
        public MessageDetail Message { get; set; }
    }
}
