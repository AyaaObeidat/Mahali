using Mahali.Data;
using Mahali.Models;
using Mahali.Repositories.Implementations;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MahaliDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("MahaliConnectionString")));

//Repositories
builder.Services.AddScoped<ICustomerInterface,CustomerInterface>();
builder.Services.AddScoped<ICategoryInterface, CategoryInterface>();
builder.Services.AddScoped<ICartInterface, CartInterface>();
builder.Services.AddScoped<ICartProductsInterface, CartProductsInterface>();
builder.Services.AddScoped<IWishListInterface, WishListInterface>();
builder.Services.AddScoped<IWishListProductsInterface, WishListProductsInterface>();
builder.Services.AddScoped<IProductInterface, ProductInterface>();
builder.Services.AddScoped<IShopInterface, ShopInterface>();
builder.Services.AddScoped<ILocationInterface, LocationInterface>();
builder.Services.AddScoped<IOrderInterface, OrderInterface>();
builder.Services.AddScoped<IOrderProductsInterface, OrderProductsInterface>();
builder.Services.AddScoped<IShopOrdersInterface, ShopOrdersInterface>();
builder.Services.AddScoped<IReviewRequestInterface, ReviewRequestInterface>();
builder.Services.AddScoped<IDiscountInterface, DiscountInterface>();
builder.Services.AddScoped<ILatestProductVisitedInterface, LatestProductsVisitedInterface>();
builder.Services.AddScoped<IAdminInterface, AdminInterface>();
builder.Services.AddScoped<IReportInterface, ReportInterface>();
builder.Services.AddScoped<IShopRequestInterface, ShopReqestInterface>();
builder.Services.AddScoped<IProductColorInterface, ProductColorInterface>();
builder.Services.AddScoped<IProductSizeInterface, ProductSizeInterface>();
builder.Services.AddScoped<IAboutUsInterface, AboutUsInterface>();

//Services
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductColorService>();
builder.Services.AddScoped<ProductSizeService>();
builder.Services.AddScoped<DiscountService>();
builder.Services.AddScoped<WishListService>();
builder.Services.AddScoped<CheckOutOpertaionService>();
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
