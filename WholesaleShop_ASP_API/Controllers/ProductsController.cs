using Blazorise.States;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WholesaleShop.DTOS;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductsDtos>> GetAll()
        {
            try
            {
                IEnumerable<Product> products = _productService.GetAllProductsWithInvoices();
                var productDtos = products.Select(p => new ProductsDtos
                {
                    Name = p.Name,
                    Code = p.Code,
                    UnitPrice = p.UnitPrice,
                    CostPrice = p.CostPrice,
                    StockQuantity = p.StockQuantity,
                });
                return Ok(productDtos);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

        [HttpGet("{uid}")]
        public ActionResult<Product> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest(ModelState);
                var product = _productService.GetByUid(uid);
                if (product == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404
                return Ok(product); // 200
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(ProductsDtos productdtos)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var product = new Product();
                {
                    product.Name = productdtos.Name;
                    product.Code = productdtos.Code;
                    product.UnitPrice = productdtos.UnitPrice;
                    product.CostPrice = productdtos.CostPrice;
                    product.StockQuantity = productdtos.StockQuantity;
                    var isCreated = _productService.Create(product);
                    if (isCreated)
                        //  return CreatedAtAction(nameof(GetByUid), new { uid = category.Uid }, category); // 201
                        return Ok("تم انشاء الفئه بنجاح"); // 200
                    return BadRequest("فشل انشاء الفئه"); // 400
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }



        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] ProdDtos product)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (product == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _productService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404
            // نضمن التحديث على نفس الـUid
            var newProduct = new Product
            {
                Uid = product.Uid,
                Name = product.Name,
                Code = product.Code,
                UnitPrice = product.UnitPrice,
                CostPrice = product.CostPrice,
                StockQuantity = product.StockQuantity,
            };
            _productService.Update(uid, newProduct);
            return Ok("تم تحديث الفئة بنجاح");
        }

        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            var exists = _productService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404
            _productService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }


    }
}