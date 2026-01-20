using Microsoft.AspNetCore.Mvc;
using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(
            ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Supplier> prods = _supplierService.GetAll();
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(supplier);
                }
                _supplierService.Create(supplier);
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
            var Supp = _supplierService.GetByUid(Uid);
            return View(Supp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Supplier supplier, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(supplier);
                }
                var Suppliers = _supplierService.GetByUid(Uid);
                if (Suppliers == null)
                {
                    return NotFound("العنصر المطلوب غير موجود.");
                }
                // تحديث البيانات
                Suppliers.Name = supplier.Name;
                Suppliers.Phone = supplier.Phone;
                Suppliers.Email = supplier.Email;
                Suppliers.Address = supplier.Address;
                Suppliers.CurrentBalance = supplier.CurrentBalance;    
                _supplierService.Update(Uid, Suppliers);
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
            var Suppliers = _supplierService.GetByUid(Uid);
            return View(Suppliers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(Supplier supplier)
        {
            try
            {
                var prods = _supplierService.GetByUid(supplier.Uid);
                if (prods == null)
                {
                    return NotFound();
                }
                _supplierService.Delete(supplier.Uid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(" An error occurred while processing your request. Please try again later or contact support for assistance ");
            }
        }

    }

}
