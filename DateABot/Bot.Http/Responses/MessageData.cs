using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class MessageData
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("files")]
        public List<object> Files { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("flow_id")]
        public string FlowId { get; set; }
    }
}
