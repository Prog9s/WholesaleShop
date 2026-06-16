using Humanizer;
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

    public class SalesInvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public SalesInvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SalesInvoiceDto>> GetAll()
        {
            try
            {
                IEnumerable<SalesInvoice> invoices = _invoiceService.GetAllSalesInvoices();
                var Dtos = invoices.Select(s => new SalesInvoiceDto
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    InvoiceDate = s.InvoiceDate,
                    TotalAmount = s.TotalAmount,
                    PaidAmount = s.PaidAmount,
                    RaminingAmount = s.RaminingAmount,
                    Notes = s.Notes,
                    // تحويل العناصر أيضاً لتجنب أي Loop
                    Items = s.SalesInvoiceItems?.Select(i => new SalesItemDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId ?? 0,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        LineTotal = i.LineTotal
                    }
                    ).ToList() ?? new List<SalesItemDto>()
                }
                ).ToList();
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
        public ActionResult Create(SalesInvoiceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1. تحويل الـ DTO إلى Entity (Mapping)
            var invoice = new SalesInvoice
            {
                CustomerId = dto.CustomerId,
                InvoiceDate = dto.InvoiceDate,
                TotalAmount = dto.TotalAmount,
                PaidAmount = dto.PaidAmount,
                RaminingAmount = dto.RaminingAmount,
                Notes = dto.Notes,
                // تحويل قائمة العناصر
                SalesInvoiceItems = dto.Items.Select(i => new SalesInvoiceItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal
                }).ToList()
            };

            // 2. إرسال الكائن الكامل للخدمة
            var success = _invoiceService.CreateSalesInvoice(invoice);

            return success ? Ok("تم حفظ الفاتورة وعناصرها بنجاح") : BadRequest("فشل الحفظ");
        }

        [HttpPut("{uid}")]
        public ActionResult Update(string uid, [FromBody] SalesInvoiceDto invoiceDtos)
        {

            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (invoiceDtos == null)
                return BadRequest("Invoice data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingInvoice = _invoiceService.GetSalesInvoiceByUid(uid);
            if (existingInvoice == null)
                return NotFound("لا توجد فاتوره بهذا الرقم"); // 404
            // نضمن التحديث على نفس الـ Uid
            var SalInvoice = new SalesInvoice
            {
                CustomerId = invoiceDtos.CustomerId,
                InvoiceDate = invoiceDtos.InvoiceDate,
                TotalAmount = invoiceDtos.TotalAmount,
                PaidAmount = invoiceDtos.PaidAmount,
                RaminingAmount = invoiceDtos.RaminingAmount,
                Notes = invoiceDtos.Notes,
                SalesInvoiceItems = invoiceDtos.Items.Select(i => new SalesInvoiceItem
                {
                    Id = i.Id, // مهم جداً للتفرقة بين الجديد والقديم
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal
                }).ToList()
            };
            var Salseresult = _invoiceService.UpdateSalesInvoice(uid, SalInvoice);
            if (Salseresult) return Ok("تم تحديث الفاتورة وجميع عناصرها");
            return NotFound("الفاتورة غير موجودة");
        }

        [HttpDelete("{uid}")]
        public ActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");
            var existingInvoice = _invoiceService.GetSalesInvoiceByUid(uid);
            if (existingInvoice == null)
                return NotFound("لا توجد فاتوره بهذا الرقم"); // 404
            _invoiceService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204

        }

    }

}
