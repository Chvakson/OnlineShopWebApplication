using GameOnlineShop.Db;
using GameOnlineShop.Db.Models;
using GameOnlineShop.Db.Repositories.Carts;
using GameOnlineShop.Db.Repositories.Orders;
using GameOnlineShop.Db.Repositories.Products;
using GameOnlineShop.Repositories.ComparedProducts;
using GameOnlineShop.Repositories.FavoriteProducts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Shop"));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connection));

builder.Services
    .AddIdentity<User, IdentityRole>() // ← Подключаем Identity
    .AddEntityFrameworkStores<ApplicationContext>() // ← Говорим где хранить пользователей
    .AddDefaultTokenProviders(); // ← Для сброса паролей и т.д.

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/SignIn";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true,
    };
});

builder.Services.AddControllersWithViews();
builder.Services.Configure<GameOnlineShop.Services.EmailSettings>(
    builder.Configuration.GetSection(GameOnlineShop.Services.EmailSettings.SectionName));
builder.Services.AddTransient<GameOnlineShop.Services.IOrderEmailService, GameOnlineShop.Services.OrderEmailService>();
builder.Services.AddTransient<IProductsDbRepository, ProductsDbRepository>();
builder.Services.AddTransient<ICartsDbRepository, CartsDbRepository>();
builder.Services.AddTransient<IOrdersDbRepository, OrdersDbRepository>();
builder.Services.AddTransient<IFavoriteDbRepository, FavoriteDbRepository>();
builder.Services.AddTransient<IComparedDbRepository, ComparedDbRepository>();


var app = builder.Build();
var runningInContainer = string.Equals(
    Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
    "true",
    StringComparison.OrdinalIgnoreCase);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    const int maxAttempts = 15;

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await DbInitializer.Initialize(context, userManager, roleManager);
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts)
        {
            logger.LogWarning(ex, "База данных ещё не готова, попытка {Attempt}/{MaxAttempts}", attempt, maxAttempts);
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Не удалось инициализировать базу данных.");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    if (!runningInContainer)
    {
        app.UseHsts();
    }
}

app.UseSerilogRequestLogging();

if (!runningInContainer)
{
    app.UseHttpsRedirection();
}

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
