using System;
using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class Mail
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("sourceArn")]
        public string SourceArn { get; set; }

        [JsonProperty("sendingAccountId")]
        public string SendingAccountId { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("destination")]
        public string[] Destination { get; set; }

        [JsonProperty("headersTruncated")]
        public bool HeadersTruncated { get; set; }

        [JsonProperty("headers")]
        public Header[] Headers { get; set; }

        [JsonProperty("commonHeaders")]
        public CommonHeaders CommonHeaders { get; set; }

        [JsonProperty("tags")]
        public Tags Tags { get; set; }
    }
}