using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly HttpClient _httpClient;

        public PredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetPredictionAsync(PredictionPythonDto data)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/predict", data);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PredictionResult>();
            return result.Prediction;
        }

        public class PredictionResult
        {
            public int Prediction { get; set; }
        }

        public class MyDataModel
        {
            public double AirTemperature { get; set; }
            public double ProcessTemperature { get; set; }
            public double RotationalSpeed { get; set; }
            public double Torque { get; set; }
            public int ToolWear { get; set; }
        }
    }
}