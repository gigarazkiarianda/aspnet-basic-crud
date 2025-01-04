using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDB.Data;
using ProductDB.Models;

namespace ProductDB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            // Get all products from the database
            var products = await _context.Products.ToListAsync();
            return View(products);  // Return to Index view with products data
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the ID is provided
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);  // Get product by ID

            // Check if product is not found
            if (product == null)
            {
                return NotFound();
            }

            return View(product);  // Return product details view
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();  // Show the form to create a new product
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Stock")] Product product)
        {
            // If model is valid, add the product to the database
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Redirect to Index page after successful creation
            }
            return View(product);  // Return the same Create form with validation errors
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the ID is provided
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);  // Find the product by ID

            // If product is not found, return NotFound
            if (product == null)
            {
                return NotFound();
            }

            return View(product);  // Return the Edit form with the product details
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock")] Product product)
        {
            // If the product ID doesn't match, return NotFound
            if (id != product.Id)
            {
                return NotFound();
            }

            // If the model state is valid, update the product
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();  // Save the changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Redirect to the Index view after successful update
            }
            return View(product);  // Return the Edit form with validation errors
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the ID is provided
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);  // Get the product to be deleted

            // If product is not found, return NotFound
            if (product == null)
            {
                return NotFound();
            }

            return View(product);  // Return the Delete confirmation view
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);  // Find the product by ID

            if (product != null)
            {
                _context.Products.Remove(product);  // Remove the product from the database
                await _context.SaveChangesAsync();  // Save the changes
            }

            return RedirectToAction(nameof(Index));  // Redirect to the Index view after deletion
        }
    }
}
