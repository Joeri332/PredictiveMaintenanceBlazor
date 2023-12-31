using System.Text.Json.Serialization;

namespace PredictiveMaintenance.Models
{
    public class PredictionResult
    {
        [JsonPropertyName("classification_report")]
        public ClassificationReport ClassificationReport { get; set; }

        [JsonPropertyName("roc_auc_score")]
        public double RocAucScore { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class ClassificationReport
    {
        [JsonPropertyName("0.0")]
        public MetricDetails Zero { get; set; }

        [JsonPropertyName("1.0")]
        public MetricDetails One { get; set; }

        [JsonPropertyName("2.0")]
        public MetricDetails Two { get; set; }

        [JsonPropertyName("3.0")]
        public MetricDetails Three { get; set; }

        [JsonPropertyName("4.0")]
        public MetricDetails Four { get; set; }

        [JsonPropertyName("accuracy")]
        public double Accuracy { get; set; }

        [JsonPropertyName("macro avg")]
        public MetricDetails MacroAvg { get; set; }

        [JsonPropertyName("weighted avg")]
        public MetricDetails WeightedAvg { get; set; }
    }

    public class MetricDetails
    {
        [JsonPropertyName("f1-score")]
        public double F1Score { get; set; }

        [JsonPropertyName("precision")]
        public double Precision { get; set; }

        [JsonPropertyName("recall")]
        public double Recall { get; set; }

        [JsonPropertyName("support")]
        public double Support { get; set; }
    }

    public class AverageDetails
    {
        [JsonPropertyName("f1-score")]
        public double F1Score { get; set; }

        [JsonPropertyName("precision")]
        public double Precision { get; set; }

        [JsonPropertyName("recall")]
        public double Recall { get; set; }

        [JsonPropertyName("support")]
        public double Support { get; set; }
    }
}
