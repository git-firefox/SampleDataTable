using DataTableBS4.Models;

namespace DataTableBS4.Services.IServices
{
    public interface IPersonService
    {
        void AddPerson(Person person);

        IEnumerable<Person> GetAllPersons();

        Person? GetPersonById(int id);

        void UpdatePerson(int id, Person updatedPerson);

        void DeletePerson(int id);
    }
}
