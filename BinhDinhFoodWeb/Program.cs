using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain;
using BinhDinhFood.Infrastructure;
using BinhDinhFood.Infrastructure.Hubs;
using BinhDinhFood.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddSingleton(configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BinhDinhFoodDbContext>(options =>
        options.UseSqlServer(configuration.ConnectionStrings.DefaultConnection));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddScoped<BinhDinhFoodDbContextInitializer>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddServerSideBlazor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication("Signin")
    .AddCookie("Signin", options =>
    {
        options.AccessDeniedPath = new PathString("/Account/Access");
        options.Cookie = new CookieBuilder
        {
            //Domain = "",
            HttpOnly = true,
            Name = "SigninCookie",
            Path = "/",
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        options.Events = new CookieAuthenticationEvents
        {
            OnSignedIn = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnSignedIn", context.Principal.Identity.Name);
                return Task.CompletedTask;
            },
            OnSigningOut = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnSigningOut", context.HttpContext.User.Identity.Name);
                return Task.CompletedTask;
            },
            OnValidatePrincipal = context =>
            {
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                    "OnValidatePrincipal", context.Principal.Identity.Name);
                return Task.CompletedTask;
            }
        };
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.LoginPath = new PathString("/User/Login");
        options.LogoutPath = "/User/Logout";
        options.ReturnUrlParameter = "ReturnUrl";
        options.SlidingExpiration = true;
    });


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var emailConfig = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddControllers();

// signal R count number customer online
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder => builder.WithOrigins("http://binhdinhfood-001-site1.dtempurl.com/").AllowAnyHeader().WithMethods("GET", "POST").AllowCredentials()));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var initialize = scope.ServiceProvider.GetRequiredService<BinhDinhFoodDbContextInitializer>();
await initialize.InitializeAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// handler error 404 page
app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode = {0}");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseSession();

// belong to signal R
app.UseCors();

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
