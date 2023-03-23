using Fridges.Application.Services.Implementations;
using Fridges.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string host = "https://localhost:7256/api/";

builder.Services.AddHttpClient("Fridges", httpClient =>
{
    httpClient.BaseAddress = new Uri(host + "fridges/");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IFridgeService, FridgeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
