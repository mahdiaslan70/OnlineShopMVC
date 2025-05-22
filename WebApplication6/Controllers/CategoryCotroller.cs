using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            var newCategory = new Category { Name = name };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return View("Index", _context.Categories.ToList());
        }

        public async Task<IActionResult> Edit(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, string Name)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) return NotFound();
            category.Name = Name;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public IActionResult Products(int categoryId)
        {
            var products = _context.Products.Where(c=>c.CategoryId == categoryId).ToList();

            return View(products);

        }
    }
}
