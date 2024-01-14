using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Interfaces
{
    public interface IScoreService
    {
        Task<List<ModelCreationScores>> GetAllSCoresAsync();
        Task<ModelCreationScores> GetScoreForModel(string fileName);
        Task<bool> CreateScoreForModel(ModelCreationScores newData);
    }
}
