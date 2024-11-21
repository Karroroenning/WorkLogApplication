using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkLogApplication2.Models;


namespace WorkTimeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(e => true).ToList();

            return View(employees);

        }

        public IActionResult CreateEmployee()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            collection.InsertOne(employee);

            return Redirect("/Employee");

        }

        public IActionResult ShowEmployee(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault();

            return View(employee);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string Id)
        {

            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            collection.DeleteOne(e => e.Id == employeeId);

            return Redirect("/Employee");

        }

        public ActionResult EditEmployee(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault();

            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(string Id, Employee employee)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");


            employee.Id = employeeId;
            collection.ReplaceOne(e => e.Id == employeeId, employee);

            return Redirect("/Employee");
        }


    }
}
