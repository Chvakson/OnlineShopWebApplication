using OnlineShopWebApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IProductsStorage, ProductsInMemoryStorage>();

builder.Services.AddSingleton<ICartsStorage, CartsInMemoryStorage>();

builder.Services.AddSingleton<IOrdersStorage, OrdersInMemoryStorage>();

builder.Services.AddSingleton<IFavoriteStorage, FavoriteInMemoryStorage>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
