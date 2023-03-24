using Fridges.Client.Services.Implementations;
using Fridges.Client.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string host = "https://localhost:7256/api/";

builder.Services.AddHttpClient("Fridges", httpClient =>
{
    httpClient.BaseAddress = new Uri(host + "fridges/");
});
builder.Services.AddHttpClient("Products", httpClient =>
{
    httpClient.BaseAddress = new Uri(host + "products/");
});
builder.Services.AddHttpClient("FridgeModels", httpClient =>
{
    httpClient.BaseAddress = new Uri(host + "fridgemodels/");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IFridgeService, FridgeService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IFridgeModelService, FridgeModelService>();

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
