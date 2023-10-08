using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private ApplicationDbContext _context;
        private IHostingEnvironment _he;
        public ProductController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _he = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Products.Include(c => c.ProductTypes).ToList());
        }
        [HttpPost]
        public IActionResult Index(decimal lowAmount, decimal largeAmount)
        {
            if (lowAmount != null && largeAmount != null)
            {
               var products= _context.Products.Include(c=>c.ProductTypes).Where(x=> x.Price>=lowAmount && x.Price<= largeAmount).ToList(); 
                return View(products);
            }
            else
            {
               return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Create()
        {
            ViewData["productTypeId"]=new SelectList(_context.ProductsTypes.ToList(),"Id","ProductType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products product,IFormFile? image)
        {
            var errors = ModelState
     .Where(x => x.Value.Errors.Count > 0)
     .Select(x => new { x.Key, x.Value.Errors })
     .ToArray();



            if (ModelState.IsValid)
            {
                var SearchProductName = _context.Products.FirstOrDefault(x => x.Name == product.Name);
                var SearchProductColor = _context.Products.FirstOrDefault(x => x.Name == product.Name);
                if (SearchProductName != null && SearchProductColor!=null)
                {
                    ViewBag.message = "This Product Already Exist";
                    ViewData["productTypeId"] = new SelectList(_context.ProductsTypes.ToList(), "Id", "ProductType");
                    return View(product);

                }
                if (image != null)
                {

                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    
                    await image.CopyToAsync(new FileStream(name,FileMode.Create));      
                    product.Image="Images/"+image.FileName;  
                }
                else
                {
                    product.Image = "Images/noimg.jpg";
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }
        // get edit action result 
        public IActionResult Edit(int? id) {
            var product = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(x => x.Id == id);
            ViewData["productTypeId"] = new SelectList(_context.ProductsTypes.ToList(), "Id", "ProductType");
            if (id == null)
            {
                return NotFound();  
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products product, IFormFile? image)
        {
            var errors = ModelState
     .Where(x => x.Value.Errors.Count > 0)
     .Select(x => new { x.Key, x.Value.Errors })
     .ToArray();

            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));

                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                else
                {
                    product.Image = "Images/noimg.jpg";
                }
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var product=_context.Products.Include(c=>c.ProductTypes).ToList().FirstOrDefault(x=>x.Id==id);
            if (product == null) { return NotFound(); }
            
            return View(product);

        }
        public IActionResult Delete(int id) 
        {
            if (id == null)
            {
                return NotFound();

            }
            var product = _context.Products.Include(c=>c.ProductTypes).FirstOrDefault(x => x.Id == id);
            if (product == null) { return NotFound(); }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Products product,int id)
        {
            if (ModelState.IsValid) 
            { 
               
                if (product != null)
                {    if (product.Id == id)
                    {
                        _context.Products.Remove(product);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                   
                }
                else
                {
                    return NotFound();
                }

            }
            return View(product);
        }
    }
}
