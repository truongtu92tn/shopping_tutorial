using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
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


var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

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



var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeendData.seedingData(context);
app.Run();
