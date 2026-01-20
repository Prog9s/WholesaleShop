using WholesaleShop.Models.Entities;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;

namespace WholesaleShop.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PurchaseInvoice> GetAllPurchaseInvoices()
        {
            return _unitOfWork._PurchaseInvoiceRepository.GetAll(
                S => S.Supplier
                );
        }
        public PurchaseInvoice? GetPurchaseInvoiceByUid(string Uid)
        {
            return _unitOfWork._PurchaseInvoiceRepository.GetByUid(Uid);

        }
        public bool CreatePurchaseInvoice(PurchaseInvoice invoice)
        {
            _unitOfWork._PurchaseInvoiceRepository.Add(invoice);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdatePurchaseInvoice(string Uid, PurchaseInvoice invoice)
        {
            var Invoice = _unitOfWork._PurchaseInvoiceRepository.GetByUid(Uid);
            if (Invoice == null)
            {
                return false;
            }
            //_unitOfWork._PurchaseInvoiceRepository.Update(invoice);
            _unitOfWork.Save();
            return true;
        }
        //public bool UpdatePurchaseInvoice(string Uid, PurchaseInvoice invoice)
        //{
        //    // 1. جلب الكائن الأصلي (المتتبع حالياً بواسطة Context)
        //    var existingInvoice = _unitOfWork._PurchaseInvoiceRepository.GetByUid(Uid);

        //    if (existingInvoice == null)
        //    {
        //        return false;
        //    }

        //    // 2. نقل القيم من الكائن القادم من الشاشة إلى الكائن المتتبع
        //    existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
        //    existingInvoice.InvoiceDate = invoice.InvoiceDate;
        //    existingInvoice.TotalAmount = invoice.TotalAmount;
        //    existingInvoice.PaidAmount = invoice.PaidAmount;
        //    existingInvoice.RaminingAmount = invoice.RaminingAmount;
        //    existingInvoice.SupplierId = invoice.SupplierId;
        //    existingInvoice.Notes = invoice.Notes;

        //    // 3. الحفظ فقط (لا داعي لاستدعاء Update لأن الكائن متتبع بالفعل)
        //    _unitOfWork.Save();
        //    return true;
        //}


        public bool DeletePurchase(string Uid)
        {
            var invoice = _unitOfWork._PurchaseInvoiceRepository.GetByUid(Uid);
            if (invoice == null)
            {
                return false;
            }
            _unitOfWork._PurchaseInvoiceRepository.Delete(invoice.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<SalesInvoice> GetAllSalesInvoices()
        {
            return _unitOfWork._SalesInvoiceRepository.GetAll(p => p.Customer);
        }

        public SalesInvoice? GetSalesInvoiceByUid(string Uid)
        {
            return _unitOfWork._SalesInvoiceRepository.GetByUid(Uid);
        }
        //public bool CreateSalesInvoice(SalesInvoice invoice)
        //{
        //    _unitOfWork._SalesInvoiceRepository.Add(invoice);
        //    _unitOfWork.Save();
        //    return true;
        //}
        public bool CreateSalesInvoice(SalesInvoice invoice)
        {
            try
            {
                // هنا يمكنك إضافة منطق التأكد من توفر المخزون قبل الحفظ
                _unitOfWork._SalesInvoiceRepository.Add(invoice);
                _unitOfWork.Save();
                return true;
            }
            catch
            {
                // يفضل تسجيل الخطأ هنا (Logging)
                return false;
            }
        }

        public bool UpdateSalesInvoice(string Uid, SalesInvoice invoice)
        {
            var Invoice = _unitOfWork._SalesInvoiceRepository.GetByUid(Uid);
            if (Invoice == null)
            {
                return false;
            }
            _unitOfWork._SalesInvoiceRepository.Update(invoice);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteSales(string Uid)
        {
            var invoice = _unitOfWork._SalesInvoiceRepository.GetByUid(Uid);
            if (invoice == null)
            {
                return false;
            }
            _unitOfWork._SalesInvoiceRepository.Delete(invoice.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _unitOfWork._CustomerRepository.GetAll();

        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _unitOfWork._SupplierRepository.GetAll();
        }
    }
}
