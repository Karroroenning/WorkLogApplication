using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkLogApplication2.Models;


namespace WorkTimeApplication.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");

            List<Work> works = collection.Find(w => true).ToList();

            return View(works);
        }

        public IActionResult CreateWork()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(l => true).ToList();

            return View(employees);

        }

        [HttpPost]
        public IActionResult CreateWork(Work work)
        {

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");

            collection.InsertOne(work);

            return Redirect("/Work");

        }

        public IActionResult ShowWork(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");

            Work work = collection.Find(w => w.Id == workId).FirstOrDefault();

            return View(work);
        }

        [HttpPost]
        public ActionResult DeleteWork(string Id)
        {

            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");

            collection.DeleteOne(w => w.Id == workId);

            return Redirect("/Work");

        }

        public ActionResult EditWork(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");
            var employeesCollection = database.GetCollection<Employee>("employees");


            Work work = collection.Find(w => w.Id == workId).FirstOrDefault();
            ViewBag.Employees = employeesCollection.Find(e => true).ToList();

            return View(work);
        }

        [HttpPost]
        public IActionResult EditWork(string Id, Work work)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("worklog");
            var collection = database.GetCollection<Work>("works");


            work.Id = workId;
            collection.ReplaceOne(w => w.Id == workId, work);

            return RedirectToAction("Index");
        }

    }
}
