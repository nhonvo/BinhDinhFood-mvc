using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain;
using BinhDinhFood.Infrastructure;
using BinhDinhFood.Infrastructure.Hubs;
using BinhDinhFood.Infrastructure.Repositories;
using BinhDinhFoodWeb.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get configuration
var configuration = builder.Configuration.Get<AppSettings>() ?? throw new Exception("Failed to load appsettings.json");
builder.Services.AddSingleton(configuration);

// ---------- Services ----------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers();

// ---------- DB Context ----------
builder.Services.AddDbContext<BinhDinhFoodDbContext>(options =>
    options.UseSqlServer(configuration.ConnectionStrings.DefaultConnection));

// ---------- Repositories ----------
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IProductRatingRepository, ProductRatingRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<BinhDinhFoodDbContextInitializer>();

// ---------- Caching & Sessions ----------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ---------- Authentication ----------
builder.Services.AddAuthentication("Signin")
    .AddCookie("Signin", options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
        options.AccessDeniedPath = "/Account/Access";
        options.ReturnUrlParameter = "ReturnUrl";
        options.SlidingExpiration = true;
        options.Cookie = new CookieBuilder
        {
            Name = "SigninCookie",
            Path = "/",
            HttpOnly = true,
            SecurePolicy = CookieSecurePolicy.SameAsRequest,
            SameSite = SameSiteMode.Lax
        };
        options.Events = new CookieAuthenticationEvents
        {
            OnSignedIn = context =>
            {
                Console.WriteLine($"{DateTime.Now} - OnSignedIn: {context.Principal.Identity?.Name}");
                return Task.CompletedTask;
            },
            OnSigningOut = context =>
            {
                Console.WriteLine($"{DateTime.Now} - OnSigningOut: {context.HttpContext.User.Identity?.Name}");
                return Task.CompletedTask;
            },
            OnValidatePrincipal = context =>
            {
                Console.WriteLine($"{DateTime.Now} - OnValidatePrincipal: {context.Principal.Identity?.Name}");
                return Task.CompletedTask;
            }
        };
    });

// ---------- SignalR & CORS ----------
builder.Services.AddSignalR();
// builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//         policy.WithOrigins("http://binhdinhfood-001-site1.dtempurl.com/")
//               .AllowAnyHeader()
//               .WithMethods("GET", "POST")
//               .AllowCredentials()));

// ---------- Email ----------
builder.Services.AddTransient<IMailService, MailService>();

// ---------- Blazor Server ----------
builder.Services.AddServerSideBlazor();

// ---------- App Initialization ----------
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<BinhDinhFoodDbContextInitializer>();
    await initializer.InitializeAsync();
}

// ---------- Middleware Pipeline ----------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // <-- must come before Authorization
app.UseAuthorization();

app.UseSession();
app.UseCors(); // <-- for SignalR

// ---------- Routes ----------
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=AdmAccount}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<CustomerHub>("/hubs/customerCount");
app.MapHub<AdminHub>("/hubs/adminHub");

app.Run();
