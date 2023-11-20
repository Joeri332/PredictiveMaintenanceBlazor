using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Database;
using PredictiveMaintenance.Interfaces;
using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly ApplicationDbContext _context;

        public PeopleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<bool> CreatePersonAsync(Person newPerson)
        {
            _context.People.Add(newPerson);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePersonAsync(Person updatedPerson)
        {
            _context.Entry(updatedPerson).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null) return false;

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
