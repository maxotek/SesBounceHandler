using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class CommonHeaders
    {
        [JsonProperty("from")]
        public string[] From { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("to")]
        public string[] To { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}