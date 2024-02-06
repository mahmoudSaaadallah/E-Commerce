using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // here we will create an object from applicationDbContext to access database to get data to view it
        //   in the web page.
        // We have to know that accessing the ApplicationdbContext class is so simple as we already inject this class in the
        //   the Program.cs using the bulider.
        private readonly IUnitOfWork _unitOfWork;
        private static string Name = "";
        private static int display = 0;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        // Now after creating the object lets get data inside the IActionREsult Index method to have the
        //   ability to use this data in the UI view file.
        public IActionResult Index()
        {
            // Inside the Action function we could get all the data from the Category table and we will pass them to the View.
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        // Here we repeate the method IActionResult Create, and used the DataAnnotation of type HttpPost and make this 
        //   this function accept an object of type Category. 
        // After we accept this object we will add it to the tabe Categories in the data base this will happen inside the method.
        // The dataAnnotation [HttpPost] must be use here to avoid ambiguate between the frist creat and the second create method.
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // We have to know that all the validation that we will do here in this method will be in server side 
            // Also we could most of this validation in the client side using jqurey using JS.
            // To do the client validation we will use the _ValidationScriptsPartial file in shared views folder.
            // but first we have to add it in our file which is Create.cshtml.
            var displayorder = _unitOfWork.Category.Select(c => c.DisplayOrder);
            var categoryName = _unitOfWork.Category.Select(c => c.Name);
            // To add the object that we alread accept from the view file of Create we have to add it to data base 
            // To do so we have to use the object of ApplicationDbContext.
            // We will use the if condation to check if the name of the category contain letters or not if not it never save in the data base else it will be saved.
            if (obj.Name.IsNullOrEmpty() || !obj.Name.Any(char.IsLetter))
            {
                ModelState.AddModelError("Name", "The Category Name must contaion Letters.");
            }
            // Then we have to check if there is a dublicate data in Name column in Category table.
            if (categoryName.Contains(obj.Name))
            {
                ModelState.AddModelError("Name", "This Category Name already exist");
            }
            // Then we have to check if there is a dublicate data in DisplayOrder column in Category table.
            if (displayorder.Contains(obj.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "This Category order already used");
            }
            // We will use if condation to check if the data is vlaid or not before we add it to database to avoid the errors.
            // The ModelState here will check the data annotaion that we used in the Category model.
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Category created successfully.";
                return RedirectToAction("Index", "Category");
            }

            // These lines of code will add the object to data base 
            // But we have to use the SaveChanges method to same the changes to data base.
            return View();
        }

        // We will make a new IActionResult Method for editing.
        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _unitOfWork.Category.GetById(id);
            Name = category.Name;
            display = category.DisplayOrder;
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var displayorder = _unitOfWork.Category.Select(c => c.DisplayOrder);
            var categoryName = _unitOfWork.Category.Select(c => c.Name);
            if (category.Name.IsNullOrEmpty() || !category.Name.Any(char.IsLetter))
            {
                ModelState.AddModelError("Name", "The name must contaion letters");
            }
            if (categoryName.Contains(category.Name) && Name != category.Name)
            {
                ModelState.AddModelError("Name", "This name is already exist.");
            }
            if (displayorder.Contains(category.DisplayOrder) && display != category.DisplayOrder)
            {
                ModelState.AddModelError("DisplayOrder", "This order already used.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "The new Categroy already updated.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _unitOfWork.Category.GetById(id);
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            Category category = _unitOfWork.Category.GetById(id);
            _unitOfWork.Category.Remove(category);
            TempData["success"] = "The Category already deleted.";
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
