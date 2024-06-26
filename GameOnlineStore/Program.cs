using GameOnlineStore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Store"));

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IProductsStorage, ProductsInMemoryStorage>();
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
