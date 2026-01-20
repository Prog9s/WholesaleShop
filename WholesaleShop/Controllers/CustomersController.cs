using Microsoft.AspNetCore.Mvc;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{

    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Customer> Cust = _customerService.GetAll();
                return View(Cust);
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
        [ValidateAntiForgeryToken]

        public IActionResult Create(Customer custom)
        {
            try
            {
                //ModelState.Remove("Payments");
                //ModelState.Remove("SalesInvoices");
                //ModelState.Remove("Uid");
                if (!ModelState.IsValid)
                {
                    return View(custom);
                }
                _customerService.Create(custom);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // أظهر الخطأ الحقيقي بدلاً من الرسالة الثابتة لمعرفة السبب (فقط أثناء التطوير)
                return Content(ex.Message + " | " + ex.InnerException?.Message);
            }
        }


        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            var cust = _customerService.GetByUid(Uid);
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Customer custom, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(custom);
                }
                var Customers = _customerService.GetByUid(Uid);
                if (Customers == null)
                {
                    return NotFound("العنصر المطلوب غير موجود.");
                }
                // تحديث البيانات
                Customers.Name = custom.Name;
                Customers.Phone = custom.Phone;
                Customers.Email = custom.Email;
                Customers.Address = custom.Address;
                Customers.CurrentBalance = custom.CurrentBalance;
                _customerService.Update(Uid, Customers);
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
            var Customers = _customerService.GetByUid(Uid);
            return View(Customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(Customer custom)
        {
            try
            {
                var prods = _customerService.GetByUid(custom.Uid);
                if (prods == null)
                {
                    return NotFound();
                }
                _customerService.Delete(custom.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

    }


}
