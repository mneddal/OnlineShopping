using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;
using System.Diagnostics;
using X.PagedList;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        

        private ApplicationDbContext _context;
      
        public HomeController(ApplicationDbContext context)
        {
           
            _context = context;

        }

        public IActionResult Index(int?page)
        {
            return View(_context.Products.Include(c=>c.ProductTypes).ToList().ToPagedList(pageNumber:page??1,pageSize:5));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Details(int id) 
        {
            if (id == null) {
                return (NotFound());
            }
            var product = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(x=>x.Id==id);
            if (product == null) { return (NotFound()); }
            else {return View(product); }
        }
        // post detail action result
        [HttpPost]
        [ActionName("Details")]
        public IActionResult ProductDetails(int id)
        {
            List<Products>products=new List<Products>();

            if (id == null)
            {
                return (NotFound());
            }
            var product = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(x => x.Id == id);
            if (product == null) { return (NotFound()); }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products=new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public IActionResult Remove(int id)
        {

            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));


        }
        //remove to cart get action method
        [ActionName("Remove")]
        public IActionResult RemoveFromCart(int id)
        {

            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));


        }
        public IActionResult Cart()
        {
            var products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products=new List<Products>();
            }
            return View(products);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}