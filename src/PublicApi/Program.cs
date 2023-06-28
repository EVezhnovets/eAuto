using eAuto.Data;
using eAuto.Data.Context;
using Microsoft.EntityFrameworkCore;
using PublicApi.Exceptions;
using PublicApi.Services;
using Serilog;
using System;

//create Serilog

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .Build();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


// Add services to the container
builder.Services.AddControllers();

var appConnection = builder.Configuration.GetConnectionString("eAutoCatalogConnection");
builder.Services.AddDbContext<EAutoContext>(options => options.UseSqlServer(appConnection));

builder.Services.AddTransient(typeof(Repository<>));
builder.Services.AddTransient<CarsApiService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.ConfigureBuildInExceptionHandler();

app.MapControllers();

app.Run();
