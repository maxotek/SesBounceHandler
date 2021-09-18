using System;
using Newtonsoft.Json;

namespace SesBounceHandler.Models.Messaging
{
    public class Bounce
    {
        [JsonProperty("feedbackId")]
        public string FeedbackId { get; set; } = null!;

        [JsonProperty("bounceType")]
        public string BounceType { get; set; } = null!;

        [JsonProperty("bounceSubType")]
        public string BounceSubType { get; set; } = null!;

        [JsonProperty("bouncedRecipients")]
        public BouncedRecipient[] BouncedRecipients { get; set; } = null!;

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("reportingMTA")]
        public string ReportingMTA { get; set; } = null!;
    }
}