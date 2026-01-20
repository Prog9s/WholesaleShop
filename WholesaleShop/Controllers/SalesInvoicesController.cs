using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Implementations;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{
    public class SalesInvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public SalesInvoicesController(
            IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<SalesInvoice> pay = _invoiceService.GetAllSalesInvoices();
                return View(pay);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }
        private void selectList()
        {
            IEnumerable<Customer> customers = _invoiceService.GetCustomers();
            SelectList selectListItems1 = new SelectList(customers, "Id", "Name");
            ViewBag.Customers = selectListItems1;
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            selectList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(SalesInvoice sales)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(sales);
                }
                _invoiceService.CreateSalesInvoice(sales);
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
            var Payment = _invoiceService.GetSalesInvoiceByUid(Uid);
            return View(Payment);
        }

        [HttpPost]
        public IActionResult Edit(SalesInvoice sales, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(sales);
                }

                var payments = _invoiceService.GetSalesInvoiceByUid(Uid);
                if (payments == null)
                {
                    return NotFound("العنصر المطلوب غير موجود.");
                }

                // تحديث البيانات
                payments.InvoiceDate = sales.InvoiceDate;
                payments.TotalAmount = sales.TotalAmount;
                payments.PaidAmount = sales.PaidAmount;
                payments.RaminingAmount = sales.RaminingAmount;
                payments.CustomerId = sales.CustomerId;
                payments.Notes = sales.Notes;
                _invoiceService.UpdateSalesInvoice(Uid, payments);
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
            var payments = _invoiceService.GetSalesInvoiceByUid(Uid);
            return View(payments);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(SalesInvoice sales)
        {
            try
            {
                var pay = _invoiceService.GetSalesInvoiceByUid(sales.Uid);
                if (pay == null)
                {
                    return NotFound();
                }
                _invoiceService.DeleteSales(sales.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }


    }

}
