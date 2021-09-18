using System;
using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class Delivery
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("processingTimeMillis")]
        public int ProcessingTimeMillis { get; set; }

        [JsonProperty("recipients")]
        public string[] Recipients { get; set; }

        [JsonProperty("smtpResponse")]
        public string SmtpResponse { get; set; }

        [JsonProperty("reportingMTA")]
        public string ReportingMTA { get; set; }
    }
}