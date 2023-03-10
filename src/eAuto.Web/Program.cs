using DiConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Add services in DiConfiguration to the container
var catalogConnectionString = builder.Configuration.GetConnectionString("eAutoCatalogConnection");
var diConfigurator = new DiConfigurator(catalogConnectionString, builder.Configuration);
diConfigurator.ConfigureServices(builder.Services);

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
