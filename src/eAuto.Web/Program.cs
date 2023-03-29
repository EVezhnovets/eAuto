using DiConfiguration;
using eAuto.Domain.Interfaces;
using eAuto.Web.Utilities;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext()
	.WriteTo.Console());
builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();

//Add services in DiConfiguration to the container
var catalogConnectionString = builder.Configuration.GetConnectionString("eAutoCatalogConnection");
var diConfigurator = new DiConfigurator(catalogConnectionString);
diConfigurator.ConfigureServices(builder.Services, builder.Logging);
builder.Services.AddTransient<IImageManager, ImageManager>();

var app = builder.Build();
app.UseSerilogRequestLogging();

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
    pattern: "{area=Admin}/{controller=Car}/{action=Index}/{id?}");

app.Run();