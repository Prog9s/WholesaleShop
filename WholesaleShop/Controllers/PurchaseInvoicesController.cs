using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Implementations;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{
    public class PurchaseInvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public PurchaseInvoicesController(
            IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        private void selectList()
        {
            IEnumerable<Supplier> suppliers1 = _invoiceService.GetSuppliers();
            SelectList selectListItems2 = new SelectList(suppliers1, "Id", "Name");
            ViewBag.Suppliers = selectListItems2;
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<PurchaseInvoice> pay = _invoiceService.GetAllPurchaseInvoices(
                    );
          
                return View(pay);
            }
            catch (Exception)
            {
                return Content("An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            selectList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(PurchaseInvoice purchase)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(purchase);
                }
                
                _invoiceService.CreatePurchaseInvoice(purchase);
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
            var Payment = _invoiceService.GetPurchaseInvoiceByUid(Uid);
            return View(Payment);
        }

        //[HttpPost]
        //public IActionResult Edit(PurchaseInvoice purchase, string Uid)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(purchase);
        //        }

        //        var payments = _invoiceService.GetPurchaseInvoiceByUid(Uid);
        //        if (payments == null)
        //        {
        //            return NotFound("العنصر المطلوب غير موجود.");
        //        }

        //        // تحديث البيانات
        //        payments.InvoiceNumber = purchase.InvoiceNumber;
        //        payments.InvoiceDate = purchase.InvoiceDate;
        //        payments.TotalAmount = purchase.TotalAmount;
        //        payments.PaidAmount = purchase.PaidAmount;
        //        payments.RaminingAmount = purchase.RaminingAmount;
        //        payments.SupplierId = purchase.SupplierId;
        //        payments.Notes = purchase.Notes;
        //        _invoiceService.UpdatePurchaseInvoice(Uid, payments);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return Content("حدث خطأ أثناء معالجة الطلب. يرجى المحاولة لاحقًا أو التواصل مع الدعم.");
        //    }
        //}
        [HttpPost]
        public IActionResult Edit(PurchaseInvoice purchase) // لا داعي لـ string Uid كبارامتر إضافي إذا كان داخل purchase
        {
            try
            {
                // جلب الفاتورة الأصلية باستخدام الـ Uid القادم من الفورم
                var invoiceInDb = _invoiceService.GetPurchaseInvoiceByUid(purchase.Uid);

                if (invoiceInDb == null)
                {
                    // هذا السطر هو ما يسبب الـ 404 إذا كان الـ Uid مختلفاً
                    return NotFound("فشل العثور على الفاتورة الأصلية لتحديثها.");
                }

                // تحديث البيانات
                invoiceInDb.InvoiceNumber = purchase.InvoiceNumber;
                invoiceInDb.InvoiceDate = purchase.InvoiceDate;
                invoiceInDb.TotalAmount = purchase.TotalAmount;
                invoiceInDb.PaidAmount = purchase.PaidAmount;
                invoiceInDb.RaminingAmount = purchase.RaminingAmount;
                invoiceInDb.SupplierId = purchase.SupplierId;
                invoiceInDb.Notes = purchase.Notes;

                // الحفظ
                _invoiceService.UpdatePurchaseInvoice(purchase.Uid, invoiceInDb);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("خطأ: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("PurchaseInvoices/Delete/{Uid}")]
        public IActionResult Delete(string Uid)
        {
            selectList();
            var payments = _invoiceService.GetPurchaseInvoiceByUid(Uid);
            return View(payments);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(PurchaseInvoice purchase)
        {
            try
            {
                var pay = _invoiceService.GetPurchaseInvoiceByUid(purchase.Uid);
                if (pay == null)
                {
                    return NotFound();
                }
                _invoiceService.DeletePurchase(purchase.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }
        


    }

}
