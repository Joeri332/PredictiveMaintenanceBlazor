using PredictiveMaintenance.Models;
using static PredictiveMaintenance.Services.PredictionService;

namespace PredictiveMaintenance.Interfaces
{
    public interface IPredictionService
    {
        Task<int> GetPredictionAsync(PredictionPythonDto data);
    }
}
