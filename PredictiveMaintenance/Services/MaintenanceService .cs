using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Database;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PredictiveMaintenanceModel>> GetMaintenanceDataAsync()
        {
            return await _context.MaintenanceData.ToListAsync();
        }

        public async Task<PredictiveMaintenanceModel> GetMaintenanceDataByIdAsync(int id)
        {
            return await _context.MaintenanceData.FindAsync(id);
        }

        public async Task<bool> CreateMaintenanceDataAsync(PredictiveMaintenanceModel newData)
        {
            _context.MaintenanceData.Add(newData);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMaintenanceDataAsync(PredictiveMaintenanceModel updatedData)
        {
            _context.Entry(updatedData).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceDataExists(updatedData.UDI))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteMaintenanceDataAsync(int UDI)
        {
            var maintenanceData = await _context.MaintenanceData.FindAsync(UDI);
            if (maintenanceData == null) return false;

            _context.MaintenanceData.Remove(maintenanceData);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool MaintenanceDataExists(int id)
        {
            return _context.MaintenanceData.Any(e => e.UDI == id);
        }
    }
}
