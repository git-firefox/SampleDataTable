namespace DataTableBS4.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => CalculateAge();
        public float Height { get; set; }
        public float Weight { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; } 
        public string? Email { get; set; }

        public Person() { }
        public Person(int id, string? name, DateTime birthDate, float height, float weight, Gender gender, string? address = null, string? email = null)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Height = height;
            Weight = weight;
            Gender = gender;
            Address = address;
            Email = email;
        }

        public int CalculateAge() => (int)((DateTime.Today - BirthDate).TotalDays / 365.25);

    }
}
