using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shopping_tutorial.Models;
using shopping_tutorial.Repository;

var builder = WebApplication.CreateBuilder(args);
// connect db
builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectDB"]);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Option =>
{
    Option.IdleTimeout = TimeSpan.FromSeconds(30);
    Option.Cookie.IsEssential = true;
});


// Đăng ký Identity
builder.Services.AddIdentity<AppUserModel, IdentityRole>()
	.AddEntityFrameworkStores<DataContext>()
	.AddDefaultTokenProviders();

// Đăng ký các dịch vụ khác cần thiết
//Services.AddScoped<ISecurityStampValidator, SecurityStampValidator<AppUserModel>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
 //   options.Password.RequireNonAlphanumeric = true;
 //   options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 4;
 //   options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    // User settings.
    //options.User.AllowedUserNameCharacters =
    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
 //   options.User.RequireUniqueEmail = true;
});



var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "category",
    pattern: "/category/{slug?}",
    defaults: new {controller="Category",action = "Index"}
    );
app.MapControllerRoute(
    name: "brand",
    pattern: "/brand/{slug?}",
    defaults: new { controller = "Brand", action = "Index" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// add data sendding
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeendData.seedingData(context);
app.Run();
