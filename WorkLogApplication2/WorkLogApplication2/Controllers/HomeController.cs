using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using WorkLogApplication2.Models;

namespace WorkLogApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<HomeIndexViewModel> viewModel = new List<HomeIndexViewModel>();

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("worklog");
            var employeesCollection = database.GetCollection<Employee>("employees");
            var worksCollection = database.GetCollection<Work>("works");

            List<Work> works = worksCollection.Find(w => true).ToList();

            foreach (Work work in works)
            {
                ObjectId employeeId = new ObjectId(work.EmployeeId);
                Employee employee = employeesCollection.Find(e => e.Id == employeeId).FirstOrDefault();

                HomeIndexViewModel model = new HomeIndexViewModel();
                model.WorkName = work.WorkDescription;
                model.WorkTime = work.Time;
                model.EmployeeName = employee.Name;
                viewModel.Add(model);
            }

            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
