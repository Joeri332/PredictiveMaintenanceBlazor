using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System;
using PredictiveMaintenance.Constants;

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
            var response = await _httpClient.GetAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.ListCsvFiles}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Correcting the expected structure to match the Flask response
                var result = JsonConvert.DeserializeAnonymousType(content, new { files = new List<string>() });

                return result.files; // Accessing the files property which contains the list of CSV files
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        public async Task<List<string>> GetListOfModelsAsync()
        {
            var response = await _httpClient.GetAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.GetListOfModels}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Use an anonymous type to match the JSON structure
                var result = JsonConvert.DeserializeAnonymousType(content, new { models = new List<string>() });

                return result.models;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        public async Task<string> CalculateNewModelFromCsv(string csvFileName)
        {
            var response = await _httpClient.PostAsJsonAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.RetrainModel}", csvFileName);
            response.EnsureSuccessStatusCode();
            
            // Read the response content as a string.
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent; // return the response content
        }

        public async Task<bool> GetNewModelAsync()
        {
            var response = await _httpClient.PostAsJsonAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.LoadModel}", new { });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateNewCsvFile(DateTime from, DateTime toTime)
        {
            // Format dates as strings in "YYYY-MM-DD" format
            string formattedStartDate = from.ToString("yyyy-MM-dd");
            string formattedEndDate = toTime.ToString("yyyy-MM-dd");

            // Construct the query string with formatted dates
            string url = $"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.ExportCsv}?startDate={formattedStartDate}&endDate={formattedEndDate}";

            // Make a GET request to the specified URL
            var response = await _httpClient.GetAsync(url);

            // Check if the response indicates success
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> SetModelToCalc(string ModelName)
        {
            var response = await _httpClient.PostAsJsonAsync($"{PredictionServiceHelpers.ConnectionString}{PredictionServiceHelpers.SetModel}", ModelName);
            response.EnsureSuccessStatusCode();

            return true;
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
