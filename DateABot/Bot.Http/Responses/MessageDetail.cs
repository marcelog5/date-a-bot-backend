using Newtonsoft.Json;

namespace Bot.Http.Responses
{
    internal sealed class MessageDetail
    {
        [JsonProperty("text_key")]
        public string TextKey { get; set; }

        [JsonProperty("data")]
        public MessageData Data { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
