using GameOnlineStore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IProductsStorage, ProductsInMemoryStorage>();
builder.Services.AddSingleton<ICartsStorage, CartsInMemoryStorage>();
builder.Services.AddSingleton<IUsersStorage, UsersInMemoryStorage>();
builder.Services.AddSingleton<IOrdersStorage, OrdersInMemoryStorage>();
builder.Services.AddSingleton<IProductsCollection, FavoriteProductsInMemoryStorage>();
builder.Services.AddSingleton<IProductsCollection, CompareProductsInMemoryStorage>();
// Add services to the container.


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
