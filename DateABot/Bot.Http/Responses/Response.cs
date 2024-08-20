using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class Response
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
    }
}
