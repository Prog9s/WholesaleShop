using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WholesaleShop.DTOS;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService =  supplierService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<SuppliersDtos>> GetAll()
        {
            try
            {
                IEnumerable<Supplier> suppliers = _supplierService.GetAllSuppliersWithInvoices();
                    var supplierDtos = suppliers.Select(s => new SuppliersDtos
                    {
                        Name = s.Name,
                        Phone = s.Phone,
                        Email = s.Email,
                        Address = s.Address,
                        CurrentBalance = s.CurrentBalance,
                    });
                return Ok(supplierDtos);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

        [HttpGet("{uid}")]
        public ActionResult<Customer> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest(ModelState);
                var suppl = _supplierService.GetByUid(uid);
                if (suppl == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(suppl); // 200
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(SuppliersDtos supplierDtos)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var suplier = new Supplier();
                suplier.Name = supplierDtos.Name;
                suplier.Phone = supplierDtos.Phone;
                suplier.Email = supplierDtos.Email;
                suplier.Address = supplierDtos.Address;
                suplier.CurrentBalance = supplierDtos.CurrentBalance;
                var isCreated = _supplierService.Create(suplier);
                if (!isCreated)
                    return Ok(" تم إنشاء هذه الفئة بنجاح"); //200
                return BadRequest("حدث خطا اثناء انشاء الفئه"); // 400
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] SuplDtos supplier)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            if (supplier == null)
                return BadRequest("Category payload is required.");
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var exists = _supplierService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newSup = new Supplier
            {
                Uid = supplier.Uid,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                CurrentBalance = supplier.CurrentBalance
            };
            _supplierService.Update(uid, newSup);
            return Ok("تم تحديث الفئة بنجاح");
        }

        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            var exists = _supplierService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404
            _supplierService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }

    }

}
