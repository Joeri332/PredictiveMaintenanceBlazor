using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Database;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredictiveMaintenance.Services
{
    public class ScoreService : IScoreService
    {
        private readonly ApplicationDbContext _context;

        public ScoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelCreationScores> GetScoreForModel(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                // Handle or throw an exception for invalid input
                return null;
            }

            try
            {
                return  _context.Scores.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateScoreForModel(ModelCreationScores newData)
        {
            _context.Scores.Add(newData);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
