using Microsoft.EntityFrameworkCore;
using WholesaleShop.Data;
using WholesaleShop.Repositories.Implementations;
using WholesaleShop.Repositories.Interfaces;
using WholesaleShop.Services.Implementations;
using WholesaleShop.Services.Interfaces;
using WholesaleShop.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// file ApplictionDBcontext.cs 
builder.Services.AddDbContext<ApplicationDbContext>(options =>

// libary Use SQL Server database
options.UseSqlServer(connectionString));

// section 2: Configure Dependency Injection for Repositories and Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder Repository
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(IPurchaseInvoiceRepository), typeof(PurchaseInvoiceRepository));
builder.Services.AddScoped(typeof(ISalesInvoiceRepository), typeof(SalesInvoiceRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
////builderService
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
builder.Services.AddScoped(typeof(IInvoiceService), typeof(InvoiceService));
builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
builder.Services.AddScoped(typeof(ISupplierService), typeof(SupplierService));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
