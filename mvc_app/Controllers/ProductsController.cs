using Microsoft.AspNetCore.Mvc;
using mvc_app.Models;
using mvc_app.Services;

namespace mvc_app.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IServiceProducts? _serviceProducts;
        private readonly ProductContext? _productContext;

        public ProductsController(IServiceProducts? serviceProducts, ProductContext? productContext)
        {
            _serviceProducts = serviceProducts;
            _productContext = productContext;
            _serviceProducts._productContext = productContext; // Обновлено для установки контекста
        }

        public ViewResult Index() => View(_serviceProducts?.Read());

        public ViewResult Details(int id) => View(_serviceProducts?.GetById(id));

        public ViewResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _ = _serviceProducts?.Create(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _serviceProducts?.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = _serviceProducts?.Update(id, product);
                if (updatedProduct != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update product.");
                }
            }
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _serviceProducts?.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _serviceProducts?.Delete(id);
            if (result == true)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
