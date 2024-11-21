using MongoDB.Bson;

namespace WorkLogApplication2.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }

        public int BirthAge { get; set; }

        public string Name { get; set; }

        public string EmployeeId { get; set; }

    }
}
