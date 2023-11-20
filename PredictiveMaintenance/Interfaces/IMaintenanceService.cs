using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Interfaces
{
    public interface IMaintenanceService
    {
        Task<List<PredictiveMaintenanceModel>> GetMaintenanceDataAsync();
        Task<PredictiveMaintenanceModel> GetMaintenanceDataByIdAsync(int id);
        Task<bool> CreateMaintenanceDataAsync(PredictiveMaintenanceModel newData);
        Task<bool> UpdateMaintenanceDataAsync(PredictiveMaintenanceModel updatedData);
        Task<bool> DeleteMaintenanceDataAsync(int id);
    }
}
