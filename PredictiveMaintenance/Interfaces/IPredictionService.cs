using PredictiveMaintenance.Models;
using static PredictiveMaintenance.Services.PredictionService;

namespace PredictiveMaintenance.Interfaces
{
    public interface IPredictionService
    {
        Task<int> GetPredictionAsync(PredictionPythonDto data);
        Task<bool> SetModelToCalc(string model);
        Task<List<string>> GetCsvFileData(DateTime dateTime);
        Task<List<string>> GetListOfModelsAsync();
        Task<bool> CalculateNewModelFromCsv(string csvFileName);
        Task<bool> GetNewModelAsync();
    }
}
