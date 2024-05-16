using System;
using System.Collections.Generic;
using DataTableBS4.Models;
using DataTableBS4.Services.IServices;
namespace DataTableBS4.Services
{

    public class PersonService : IPersonService
    {
        private List<Person> _persons;

        public PersonService()
        {
            _persons = new List<Person>();

            for (int i = 1; i <= 100; i++)
            {
                Gender randomGender = (Gender)new Random().Next(0, 3); // Randomly choose a gender
                DateTime randomBirthDate = DateTime.Today.AddYears(-new Random().Next(18, 70)); // Random birth date between 18 and 70 years ago

                AddPerson(new Person
                {
                    Id = i,
                    Gender = randomGender,
                    Name = randomGender == Gender.Male ? GenerateRandomMaleName() : GenerateRandomFemaleName(),
                    BirthDate = randomBirthDate,
                    Height = GenerateRandomHeight(),
                    Weight = GenerateRandomWeight(),
                    Address = GenerateRandomAddress(),
                    Email = $"person{i}@example.com"
                });
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _persons;
        }

        public Person? GetPersonById(int id)
        {
            return _persons.Find(person => person.Id == id);
        }

        // Method to update a person
        public void UpdatePerson(int id, Person updatedPerson)
        {
            int index = _persons.FindIndex(person => person.Id == id);
            if (index != -1)
            {
                _persons[index] = updatedPerson;
            }
            else
            {
                throw new ArgumentException("Person not found.");
            }
        }

        // Method to delete a person
        public void DeletePerson(int id)
        {
            _persons.RemoveAll(person => person.Id == id);
        }

        #region Generate Dummy 

        private static string GenerateRandomMaleName()
        {
            string[] maleNames = { "John", "Michael", "James", "David", "Robert", "William", "Joseph", "Richard", "Thomas", "Daniel" };
            return maleNames[new Random().Next(0, maleNames.Length)];
        }

        private static string GenerateRandomFemaleName()
        {
            string[] femaleNames = { "Mary", "Jennifer", "Linda", "Patricia", "Elizabeth", "Susan", "Jessica", "Sarah", "Karen", "Nancy" };
            return femaleNames[new Random().Next(0, femaleNames.Length)];
        }

        private static float GenerateRandomHeight()
        {
            return (float)new Random().NextDouble() * (200 - 150) + 150; // Random height between 150cm and 200cm
        }

        private static float GenerateRandomWeight()
        {
            return (float)new Random().NextDouble() * (100 - 50) + 50; // Random weight between 50kg and 100kg
        }

        private static string GenerateRandomAddress()
        {
            string[] streetNames = { "Main St", "Elm St", "Maple St", "Oak St", "Pine St", "Cedar St", "Walnut St", "Cherry St", "Spruce St", "Birch St" };
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };

            return $"{new Random().Next(1, 1000)} {streetNames[new Random().Next(0, streetNames.Length)]}, {cities[new Random().Next(0, cities.Length)]}";
        }

        #endregion
    }
}
