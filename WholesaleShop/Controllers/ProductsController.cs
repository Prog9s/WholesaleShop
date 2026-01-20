using Microsoft.AspNetCore.Mvc;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{

    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Product> prods = _productService.GetAll();
                return View(prods);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                _productService.Create(product);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }


        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            var Product = _productService.GetByUid(Uid);
            return View(Product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                var Products = _productService.GetByUid(Uid);
                if (Products == null)
                {
                    return NotFound("العنصر المطلوب غير موجود.");
                }
                // تحديث البيانات
                Products.Name = product.Name;
                Products.Code = product.Code;
                Products.UnitPrice = product.UnitPrice;
                Products.CostPrice = product.CostPrice;
                Products.StockQuantity = product.StockQuantity;
                _productService.Update(Uid, Products);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("حدث خطأ أثناء معالجة الطلب. يرجى المحاولة لاحقًا أو التواصل مع الدعم.");
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            var Products = _productService.GetByUid(Uid);
            return View(Products);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            try
            {
                var prods = _productService.GetByUid(product.Uid);
                if (prods == null)
                {
                    return NotFound();
                }
                _productService.Delete(product.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

    }

}
