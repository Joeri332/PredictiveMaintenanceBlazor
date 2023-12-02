using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Database;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Services
{
    public class OperatorFeedbackService : IOperatorFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public OperatorFeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperatorFeedback>> GetAllFeedbackAsync()
        {
            return await _context.OperatorFeedbacks.ToListAsync();
        }

        public async Task<OperatorFeedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.OperatorFeedbacks.FindAsync(id);
        }

        public async Task<bool> CreateFeedbackAsync(OperatorFeedback newFeedback)
        {
            _context.OperatorFeedbacks.Add(newFeedback);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFeedbackAsync(OperatorFeedback updatedFeedback)
        {
            _context.Entry(updatedFeedback).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.OperatorFeedbacks.FindAsync(id);
            if (feedback == null) return false;

            _context.OperatorFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }  

        public async Task<List<OperatorFeedback>> GetFeedbacksWithProductIdAsync()
        {
            return await _context.OperatorFeedbacks
                       .Include(f => f.PredictiveMaintenance) // Ensure you load the related data
                       .ToListAsync();
        }
    }
}
