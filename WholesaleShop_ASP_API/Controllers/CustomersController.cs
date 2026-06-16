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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDtos>> GetAll()
        {
            try
            {
                IEnumerable<Customer> Cust = _customerService.GetAllCustomersWithInvoices();
                var custoDtos = Cust.Select(C => new CustomerDtos
                {
                    Name = C.Name,
                    Phone = C.Phone,
                    Email = C.Email,
                    Address = C.Address,
                    CurrentBalance = C.CurrentBalance,
                });
                return Ok(custoDtos);
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

                var customer = _customerService.GetByUid(uid);
                if (customer == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(customer); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }

        }

        [HttpPost]
        public ActionResult Create(CustomerDtos customerDtos)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var customer = new Customer();
                customer.Name = customerDtos.Name;
                customer.Phone = customerDtos.Phone;
                customer.Email = customerDtos.Email;
                customer.Address = customerDtos.Address;
                customer.CurrentBalance = customerDtos.CurrentBalance;
                var isCreated = _customerService.Create(customer);
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
        public IActionResult Update(string uid, [FromBody] CustDtos customer)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (customer == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _customerService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newCust = new Customer
            {
                Uid = customer.Uid,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                CurrentBalance = customer.CurrentBalance
            };
            _customerService.Update(uid, newCust);
            return Ok("تم تحديث الفئة بنجاح");
        }

        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            var exists = _customerService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404
            _customerService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }

    }
}
