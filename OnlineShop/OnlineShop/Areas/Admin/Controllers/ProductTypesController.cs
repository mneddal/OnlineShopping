using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Runtime.CompilerServices;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _context;
        public ProductTypesController(ApplicationDbContext context) 
        { 
            _context = context; 
        }
        public IActionResult Index()
        {
            return View(_context.ProductsTypes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.ProductsTypes.Add(productTypes);
                await _context.SaveChangesAsync();
                TempData["save"]="Product type saved";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productTypes);
            }
        }


        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductsTypes.Find(id);
            if (productType == null)
            {
                return (NotFound());
            }
            else
            {
                return View(productType);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.ProductsTypes.Update(productTypes);
                await _context.SaveChangesAsync();
                TempData["edit"] = "Product type editted";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productTypes);
            }
        }


        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductsTypes.Find(id);
            if (productType == null)
            {
                return (NotFound());
            }
            else
            {
                return View(productType);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Details(ProductTypes productTypes)
        {
                
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType=_context.ProductsTypes.Find(id);
            if (productType == null) {return NotFound(); }
            else {return View(productType); }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductTypes productTypes)
        {

            if (ModelState.IsValid)
            {
                _context.ProductsTypes.Remove(productTypes);
                await _context.SaveChangesAsync();
                TempData["delete"] = "Product type deleted";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

    }

    
    
}
