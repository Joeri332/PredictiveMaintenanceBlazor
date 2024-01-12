using PredictiveMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Interfaces
{
    public interface IOperatorFeedbackService
    {
        Task<List<OperatorFeedback>> GetAllFeedbackAsync();
        Task<OperatorFeedback> GetFeedbackByIdAsync(int id);
        Task<bool> CreateFeedbackAsync(OperatorFeedback newFeedback);
        Task<bool> UpdateFeedbackAsync(OperatorFeedback updatedFeedback);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<List<OperatorFeedback>> GetFeedbacksWithProductIdAsync();
    }
}
