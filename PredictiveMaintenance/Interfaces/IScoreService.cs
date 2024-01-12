using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Interfaces
{
    public interface IScoreService
    {
        Task<ModelCreationScores> GetScoreForModel(string fileName);
        Task<bool> CreateScoreForModel(ModelCreationScores newData);
    }
}
