using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using E_Commerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // here we will create an object from applicationDbContext to access database to get data to view it
        //   in the web page.
        // We have to know that accessing the ApplicationdbContext class is so simple as we already inject this class in the
        //   the Program.cs using the bulider.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        // Now after creating the object lets get data inside the IActionREsult Index method to have the
        //   ability to use this data in the UI view file.
        public IActionResult Index()
        {
            // Inside the Action function we could get all the data from the Product table and we will pass them to the View.
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }
        public IActionResult Upsert(int? id)
        {
            // Now as we have a relation between category and product tables and we want to retrive categories to display 
            //  them while we create a new product to choose a specific category to each product then we have to get all the
            //   cateories.
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
            //    .Select(c => new SelectListItem
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()
            //    });
            // Now after we get all the catgories as selectListItem then we have to display them but we can't do this using
            //  the following return view as it accept just one parameter.


            // we have something called
            // ViewBag:
            /*
             * ViewBag transfers data from the Controller to View, not vice-versa.
             * Ideal for situations in which the temporary data is not in a model.
             * ViewBag is a dynamic property that takes advantage of the new dynamic features in 4.0
             * Any number of properties and values can be assigned to ViewBag
             * The ViewBag's life only lasts during the current http request.
             * ViewBag values will be null if redirection occurs.
             * ViewBag is actually a wrapper around ViewData.
             */

            /*
             * In ASP.NET MVC, `ViewBag` is a dynamic property that allows you to pass data from a controller to a view. 
             * It's a dynamic wrapper around `ViewData`, which is a dictionary-like object that allows you to pass data between a 
             * controller and a view.
             * Here's a basic example of how you might use `ViewBag` in a controller and then access it in a corresponding view:
             * Controller:
                ```csharp
                public class HomeController : Controller
                {
                    public IActionResult Index()
                    {
                        // Set data in ViewBag
                        ViewBag.Message = "Hello from ViewBag!";
                        return View();
                    }
                }
                ```
                
             * View (Index.cshtml):
                ```html
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Home</title>
                </head>
                <body>
                    <h1>@ViewBag.Message</h1>
                </body>
                </html>
                ```

              * In this example, the controller action `Index()` sets a message in `ViewBag.Message`, and then this message is
              * accessed in the corresponding view using `@ViewBag.Message`.
              * It's important to note that `ViewBag` is dynamic, which means you don't get compile-time type checking. So, you 
              * should be careful when using it to avoid runtime errors. Additionally, it's generally recommended to use 
              * strongly-typed models (using `@model` directive) instead of `ViewBag` when possible, as it provides better 
              * compile-time safety and IntelliSense support.
              */


            //ViewBag.CategoryList = CategoryList;




            // We have another way that called 
            // ViewData
            /*
             * ViewData transfers data from the Controller to View, not vice-versa. Ideal for situations in which the temporary    data is not in a model.
             * ViewData is derived from ViewDataDictionary which is a dictionary type.
             * ViewData value must be type cast before use.
             * The ViewData's life only lasts during the current http request. 
             * ViewData values will be null if redirection occurs.
             */

            /*
             * `ViewData` is a dictionary-like object provided by ASP.NET MVC that allows you to pass data from a controller to a 
             * view.
             * It's similar to `ViewBag`, but it's not dynamic, meaning you have to use explicit casting to access its values. 
             * Unlike `ViewBag`, `ViewData` is not defined as a dynamic property.      
             * Here's how you might use `ViewData` in a controller and access it in a corresponding view:

                Controller:
                ```csharp
                public class HomeController : Controller
                {
                    public IActionResult Index()
                    {
                        // Set data in ViewData
                        ViewData["Message"] = "Hello from ViewData!";
                        return View();
                    }
                }
                ```

                View (Index.cshtml):
                ```html
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Home</title>
                </head>
                <body>
                    <h1>@ViewData["Message"]</h1>
                </body>
                </html>
                ```

               * In this example, the controller action `Index()` sets a message in `ViewData["Message"]`, and then this message 
                   is accessed in the corresponding view using `@ViewData["Message"]`.

               * Since `ViewData` is not dynamic, you need to ensure that you cast its values appropriately when retrieving them 
                  in the view. For example:

                ```html
                <h1>@((string)ViewData["Message"])</h1>
                ```

                Like `ViewBag`, it's generally recommended to use strongly-typed models instead of `ViewData` when possible for better compile-time safety and IntelliSense support.
             */




            // We have another thing called 
            // TempData
            /*
             * TempData can be used to store data between two consecutive requests.
             * TempData internaly use Session to store the data. So think of it as a short lived session.
             * TempData value must be type cast before use. Check for null values to avoid runtime error.
             * TempData can be used to store only one time messages like error messages, validation messages.
             */

            /*
             * `TempData` is another feature provided by ASP.NET MVC, similar to `ViewData` 
             * and `ViewBag`, but with a key difference: it persists data for the duration of
             * a single HTTP request, which means it's useful for passing data between 
             * controllers and their corresponding views during redirects.

                Here's how you might use `TempData`:

                Controller:
                ```csharp
                public class HomeController : Controller
                {
                    public IActionResult Index()
                    {
                        // Set data in TempData
                        TempData["Message"] = "Hello from TempData!";
                        return RedirectToAction("About");
                    }

                    public IActionResult About()
                    {
                        // Access data from TempData
                        string message = TempData["Message"] as string;
                        return View(model: message);
                    }
                }
                ```

                In this example, the `Index` action sets a message in `TempData`, and then 
                redirects to the `About` action. In the `About` action, the message is 
                retrieved from 
           TempData` and passed to the view as a model.

                View (About.cshtml):
                ```html
                <!DOCTYPE html>
                <html>
                <head>
                    <title>About</title>
                </head>
                <body>
                    <h1>@Model</h1>
                </body>
                </html>
                ```

                When you use `TempData`, the data stored in it will be available for the
            current and the subsequent request only. After that, it will be cleared 
            automatically. This is useful for scenarios like displaying a message to the user
             after a form submission, where you want the message to be displayed on the next 
                page but not on subsequent pages.

                Remember to handle null values appropriately when accessing `TempData` to 
                    avoid runtime errors.
             */
             
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()

            };
            if (id == null || id == 0)
            {
                // Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _unitOfWork.Product.GetById((int)id);
                return View(productVM);
            }
           
        }
        // Here we repeate the method IActionResult Create, and used the DataAnnotation of type HttpPost and make this 
        //   this function accept an object of type Product. 
        // After we accept this object we will add it to the tabe Categories in the data base this will happen inside the method.
        // The dataAnnotation [HttpPost] must be use here to avoid ambiguate between the frist creat and the second create method.
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            
            // We will use if condation to check if the data is vlaid or not before we add it to database to avoid the errors.
            // The ModelState here will check the data annotaion that we used in the Product model.
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "The Product created successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });     
                return View(productVM);
            }
            // These lines of code will add the object to data base 
            // But we have to use the SaveChanges method to same the changes to data base.
            
        } 
        public ActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.GetById(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            Product? product = _unitOfWork.Product.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            TempData["success"] = "The Product already deleted.";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
