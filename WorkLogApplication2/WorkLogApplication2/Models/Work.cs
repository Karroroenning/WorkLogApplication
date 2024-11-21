using MongoDB.Bson;

namespace WorkLogApplication2.Models
{
    public class Work
    {
        public ObjectId Id { get; set; }

        public int Time { get; set; }

        public string WorkDescription { get; set; }

        public string EmployeeId { get; set; }

    }
}
