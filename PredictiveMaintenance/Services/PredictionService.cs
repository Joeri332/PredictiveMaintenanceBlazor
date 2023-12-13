using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            var result = await PostJsonRequest<PredictionResult>($"{PredictionServiceHelpers.Predict}", data);
            return result.Prediction;
        }

        public async Task<List<string>> GetCsvFileData(DateTime dateTime)
        {
            return await PostJsonRequest<List<string>>($"{PredictionServiceHelpers.GetListCsvFiles}", dateTime);
        }

        public async Task<List<string>> GetListOfModelsAsync()
        {
            return await PostJsonRequest<List<string>>($"{PredictionServiceHelpers.GetListOfModels}", null);
        }

        public async Task<bool> GetNewModelAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.GetNewModel}", new { });
            return response.IsSuccessStatusCode;
        }

        private async Task<T> PostJsonRequest<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync($"{PredictionServiceHelpers.ConnectionString}{url}", data);
            response.EnsureSuccessStatusCode();

            if (response.Content is null)
            {
                throw new InvalidOperationException("Response content is null");
            }

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result is null ? throw new InvalidOperationException("Failed to deserialize response content") : result;
        }


        public class PredictionResult
        {
            public int Prediction { get; set; }
        }
    }
}
