using GameOnlineStore.Db;
using GameOnlineStore.Db.Models;
using GameOnlineStore.Db.Repositories.Carts;
using GameOnlineStore.Db.Repositories.Orders;
using GameOnlineStore.Db.Repositories.Products;
using GameOnlineStore.Repositories.ComparedProducts;
using GameOnlineStore.Repositories.FavoriteProducts;
using GameOnlineStore.Repositories.Roles;
using GameOnlineStore.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Store"));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connection));

builder.Services
    .AddIdentity<User, IdentityRole>() // ← Подключаем Identity
    .AddEntityFrameworkStores<ApplicationContext>() // ← Говорим где хранить пользователей
    .AddDefaultTokenProviders(); // ← Для сброса паролей и т.д.

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Index";
    options.AccessDeniedPath = "/Home/Index";
    options.LogoutPath = "/Home/Index";
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductsDbRepository, ProductsDbRepository>();
builder.Services.AddTransient<ICartsDbRepository, CartsDbRepository>();
builder.Services.AddTransient<IOrdersDbRepository, OrdersDbRepository>();
builder.Services.AddTransient<IFavoriteDbRepository, FavoriteDbRepository>();
builder.Services.AddTransient<IComparedDbRepository, ComparedDbRepository>();
builder.Services.AddSingleton<IRolesStorage, RolesInMemoryStorage>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Создаем роли и админа при запуске
        await DbInitializer.Initialize(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
