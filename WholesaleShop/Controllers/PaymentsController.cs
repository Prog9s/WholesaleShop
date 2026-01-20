using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{
    public class PaymentsController : Controller
    {
        //private readonly ICustomerService _customerService;
        //private readonly ISupplierService _supplierService;
        private readonly IPaymentService _paymentService;
        public PaymentsController(
            IPaymentService paymentService
            //ICustomerService customerService,
            //ISupplierService supplierService
            )
        {
            _paymentService = paymentService;
            //_customerService = customerService;
            //_supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Payment> pay = _paymentService.GetAll();
                return View(pay);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

        private void selectList()
        {
            IEnumerable<Customer> customers = _paymentService.GetCustomers();
            SelectList selectListItems1 = new SelectList(customers, "Id", "Name");
            ViewBag.Customers = selectListItems1;

            IEnumerable<Supplier>suppliers1  = _paymentService.GetSuppliers();
            SelectList selectListItems2 = new SelectList(suppliers1, "Id", "Name");
            ViewBag.Suppliers = selectListItems2;

            //var customers = _paymentService.GetAll();
            //ViewBag.Customers = new SelectList(customers, "Id", "Name");

            //var suppliers = _paymentService.GetAll();
            //ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
        }

        [HttpGet]
        public IActionResult Create()
        {
            selectList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(payment);
                }
                _paymentService.Create(payment);
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
            selectList();
            var Payment = _paymentService.GetByUid(Uid);
            return View(Payment);
        }

        [HttpPost]
        public IActionResult Edit(Payment payment, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(payment);
                }

                var payments = _paymentService.GetByUid(Uid);
                if (payments == null)
                {
                    return NotFound("العنصر المطلوب غير موجود.");
                }

                // تحديث البيانات
                payments.PaymentDate = payment.PaymentDate;
                payments.Type = payment.Type;
                payments.Amount = payment.Amount;
                payments.CustomerId = payment.CustomerId;
                payments.SupplierId = payment.SupplierId;
                payment.Notes = payment.Notes;
                _paymentService.Update(Uid, payments);
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
            selectList();
            var payments = _paymentService.GetByUid(Uid);
            return View(payments);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(Payment payment)
        {
            try
            {
                var pay = _paymentService.GetByUid(payment.Uid);
                if (pay == null)
                {
                    return NotFound();
                }
                _paymentService.Delete(payment.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

    }
}
