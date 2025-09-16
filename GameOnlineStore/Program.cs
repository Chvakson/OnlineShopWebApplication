using GameOnlineStore.Repositories.Carts;
using GameOnlineStore.Repositories.CompareProducts;
using GameOnlineStore.Repositories.FavoriteProducts;
using GameOnlineStore.Repositories.Orders;
using GameOnlineStore.Db.Repositories;
using GameOnlineStore.Repositories.Roles;
using GameOnlineStore.Repositories.Users;
using Serilog;
using GameOnlineStore.Db;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Store"));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductsDbRepository, ProductsDbRepository>();
builder.Services.AddSingleton<ICartsStorage, CartsInMemoryStorage>();
builder.Services.AddSingleton<IUsersManager, UsersManager>();
builder.Services.AddSingleton<IOrdersStorage, OrdersInMemoryStorage>();
builder.Services.AddSingleton<IFavoriteProducts, FavoriteProductsInMemoryStorage>();
builder.Services.AddSingleton<ICompareProducts ,CompareProductsInMemoryStorage>();
builder.Services.AddSingleton<IRolesStorage, RolesInMemoryStorage>();
// Add services to the container.


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
