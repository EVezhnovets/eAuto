using DiConfiguration;
using eAuto.Data.Context;
using eAuto.Domain.Interfaces;
using eAuto.Web.Utilities;
using Microsoft.EntityFrameworkCore;
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

var appConnection= builder.Configuration.GetConnectionString("eAutoCatalogConnection");
var identityConnection= builder.Configuration.GetConnectionString("eAutoIdentityConnection");

var diConfigurator = new DiConfigurator(identityConnection, appConnection, builder.Configuration);
diConfigurator.ConfigureServices(builder.Services, builder.Logging);
builder.Services.AddTransient<IImageManager, ImageManager>();

var app = builder.Build();

#region Auto Migration
try
{
	var context = app.Services.GetRequiredService<EAutoContext>();
	if (context.Database.IsSqlServer())
	{
		context.Database.Migrate();
	}
	await EntityContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
	app.Logger.LogError(ex, "An error occurred adding migrations to DatabBase.");
}
#endregion
app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/CarsCatalog/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=CarsCatalog}/{action=Index}/{id?}");

app.Run();