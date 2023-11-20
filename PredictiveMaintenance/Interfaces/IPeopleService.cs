using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Interfaces
{
    public interface IPeopleService
    {
        Task<List<Person>> GetPeopleAsync();
        Task<Person> GetPersonByIdAsync(int id);
        Task<bool> CreatePersonAsync(Person newPerson);
        Task<bool> UpdatePersonAsync(Person updatedPerson);
        Task<bool> DeletePersonAsync(int id);
    }
}
