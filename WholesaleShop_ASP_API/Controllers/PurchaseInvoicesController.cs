using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WholesaleShop.DTOS;
using WholesaleShop.Models.Entities;
using WholesaleShop.Repositories.Interfaces;
using WholesaleShop.Services.Implementations;
using WholesaleShop.Services.Interfaces;

namespace WholesaleShop_ASP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public PurchaseInvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PurchaseInvoicesDtos>> GetAll()
        {
            try
            {
                IEnumerable<PurchaseInvoice> invoices = _invoiceService.GetAllPurchaseInvoices();
                var Dtos = invoices.Select(p => new PurchaseInvoicesDtos
                {
                    Id =p.Id,
                    InvoiceNumber = p.InvoiceNumber,
                    SupplierId = p.SupplierId,
                    InvoiceDate = p.InvoiceDate,
                    TotalAmount = p.TotalAmount,
                    PaidAmount = p.PaidAmount,
                    RaminingAmount = p.RaminingAmount,
                    Notes = p.Notes,
                    // تحويل العناصر أيضاً لتجنب أي Loop
                    Items = p.PurchaseInvoiceItems?.Select(i => new PurchaseItemDtos
                    {
                        Id = i.Id,
                        PurchaseInvoiceId = i.PurchaseInvoiceId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        CostPrice = i.CostPrice,
                        LineTotal = i.LineTotal
                    }
                    ).ToList() ?? new List<PurchaseItemDtos>()
                });
                return Ok(Dtos);
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpGet("{uid}")]
        public ActionResult<PurchaseInvoice> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest(ModelState);
                var invoice = _invoiceService.GetPurchaseInvoiceByUid(uid);
                if (invoice == null)
                    return NotFound("لا توجد فاتوره بهذا الرقم"); // 404
                return Ok(invoice); // 200
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }

        }


        [HttpPost]
        public ActionResult Create(PurchaseInvoicesDtos Dtos)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
                var invoice = new PurchaseInvoice
                {
                    InvoiceNumber = Dtos.InvoiceNumber,
                    SupplierId = Dtos.SupplierId,
                    InvoiceDate = Dtos.InvoiceDate,
                    TotalAmount = Dtos.TotalAmount,
                    PaidAmount = Dtos.PaidAmount,
                    RaminingAmount = Dtos.RaminingAmount,
                    Notes = Dtos.Notes,
                    // تحويل العناصر أيضاً لتجنب أي Loop
                    PurchaseInvoiceItems = Dtos.Items.Select(i => new PurchaseInvoiceItem
                    {
                        PurchaseInvoiceId = i.PurchaseInvoiceId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        CostPrice = i.CostPrice,
                        LineTotal = i.LineTotal
                    }).ToList()
                    };
                var success = _invoiceService.CreatePurchaseInvoice(invoice);
                    return success ? Ok("تم حفظ الفاتورة وعناصرها بنجاح") : BadRequest("فشل الحفظ");
        }
        [HttpPut("{uid}")]
        public ActionResult Update(string uid, [FromBody] PurchaseInvoicesDtos invoiceDtos)
        {
            
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (invoiceDtos == null)
                return BadRequest("Invoice data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingInvoice = _invoiceService.GetPurchaseInvoiceByUid(uid);
            if (existingInvoice == null)
                return NotFound("لا توجد فاتوره بهذا الرقم"); // 404
            // نضمن التحديث على نفس الـ Uid
            var purchInvoice = new PurchaseInvoice
            {
                InvoiceNumber = invoiceDtos.InvoiceNumber,
                SupplierId = invoiceDtos.SupplierId,
                InvoiceDate = invoiceDtos.InvoiceDate,
                TotalAmount = invoiceDtos.TotalAmount,
                PaidAmount = invoiceDtos.PaidAmount,
                RaminingAmount = invoiceDtos.RaminingAmount,
                Notes = invoiceDtos.Notes,
                PurchaseInvoiceItems = invoiceDtos.Items.Select(i => new PurchaseInvoiceItem
                {
                    PurchaseInvoiceId = i.PurchaseInvoiceId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    CostPrice = i.CostPrice,
                    LineTotal = i.LineTotal
                }).ToList()
            };

           var Purchaceresult= _invoiceService.UpdatePurchaseInvoice(uid, purchInvoice);
            if (Purchaceresult) return Ok("تم تحديث الفاتورة وجميع عناصرها");
            return NotFound("الفاتورة غير موجودة");
        }


        [HttpDelete("{uid}")]
        public ActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            var existingInvoice = _invoiceService.GetPurchaseInvoiceByUid(uid);
            if (existingInvoice == null)
                return NotFound("لا توجد فاتوره بهذا الرقم"); // 404
            _invoiceService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        
        }

    }
}
