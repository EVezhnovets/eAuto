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

var appConnection= builder.Configuration.GetConnectionString("eAutoCatalogConnection");
var identityConnection= builder.Configuration.GetConnectionString("eAutoIdentityConnection");

var diConfigurator = new DiConfigurator(identityConnection, appConnection, builder.Configuration);
diConfigurator.ConfigureServices(builder.Services, builder.Logging);
builder.Services.AddTransient<IImageManager, ImageManager>();

var app = builder.Build();
app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();