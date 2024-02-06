using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // This will Run the Index file in Views Inside Home folder.
            // We have to know that the if the View method doesn't accept any paramerter then It will run
            //   the View that has the same name of the Action method in the folder of Views that Has the same name
            //   of Controller.
            // Or I could add the name of the view the I want to run, but It must be in side the Views
            //   folder inside another folder with the same name of Controller.
            // return View("Index"); like this this will return the same page of the Index.
            return View();
        }

        public IActionResult Privacy()
        {
            // This will Run the Privacy file in Views Inside Home folder.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
