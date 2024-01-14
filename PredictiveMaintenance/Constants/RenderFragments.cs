using Microsoft.AspNetCore.Components;
using PredictiveMaintenance.Enums;
using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Constants
{
    public static class RenderFragments
    {
        public static RenderFragment CreatePredictionCard(PredictionResult predictionResult) => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "card prediction-card");
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "card-header");
            builder.AddContent(4, predictionResult.ModelName);
            builder.CloseElement();
            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", "card-body");

            builder.OpenElement(7, "h5");
            builder.AddContent(8, "Status: ");
            builder.OpenElement(9, "span");
            builder.AddContent(10, predictionResult.Status);
            builder.CloseElement();
            builder.CloseElement();

            builder.OpenElement(11, "p");
            builder.AddContent(12, $"ROC AUC Score: {predictionResult.RocAucScore}");
            builder.CloseElement();

            builder.OpenElement(13, "h5");
            builder.AddContent(14, "Classification Report");
            builder.CloseElement();

            builder.OpenElement(15, "p");
            builder.AddContent(16, $"Accuracy: {predictionResult.ClassificationReport.Accuracy}");
            builder.CloseElement();

            builder.AddContent(17, DisplayMetrics("No failure", predictionResult.ClassificationReport.Zero));
            builder.AddContent(18, DisplayMetrics("Power failure", predictionResult.ClassificationReport.One));
            builder.AddContent(19, DisplayMetrics("Tool Wear Failure", predictionResult.ClassificationReport.Two));
            builder.AddContent(20, DisplayMetrics("Overstrain Failure", predictionResult.ClassificationReport.Three));
            builder.AddContent(21, DisplayMetrics("Heat Dissipation Failure", predictionResult.ClassificationReport.Four));

            // Averages
            builder.OpenElement(22, "h5");
            builder.AddContent(23, "Macro Average");
            builder.CloseElement();
            builder.AddContent(24, DisplayMetrics("Macro Avg", predictionResult.ClassificationReport.MacroAvg));

            builder.OpenElement(25, "h5");
            builder.AddContent(26, "Weighted Average");
            builder.CloseElement();
            builder.AddContent(27, DisplayMetrics("Weighted Avg", predictionResult.ClassificationReport.WeightedAvg));

            builder.CloseElement(); // Close card-body
            builder.CloseElement(); // Close card
        };

        private static RenderFragment DisplayMetrics(string title, MetricDetails metrics) => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "metric-container");

            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "metric-title");
            builder.AddContent(4, title);
            builder.CloseElement();

            builder.OpenElement(5, "p");
            builder.AddAttribute(6, "class", "metric-value");
            builder.AddContent(7, $"F1-Score: {metrics.F1Score}");
            builder.CloseElement();

            builder.OpenElement(8, "p");
            builder.AddAttribute(9, "class", "metric-value");
            builder.AddContent(10, $"Precision: {metrics.Precision}");
            builder.CloseElement();

            builder.OpenElement(11, "p");
            builder.AddAttribute(12, "class", "metric-value");
            builder.AddContent(13, $"Recall: {metrics.Recall}");
            builder.CloseElement();

            builder.OpenElement(14, "p");
            builder.AddAttribute(15, "class", "metric-value");
            builder.AddContent(16, $"Support: {metrics.Support}");
            builder.CloseElement();

            builder.CloseElement();
        };
    }
}
