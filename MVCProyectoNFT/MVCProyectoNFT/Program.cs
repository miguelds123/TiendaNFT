using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVCProyectoNFT.Application.Config;
using MVCProyectoNFT.Application.Profiles;
using MVCProyectoNFT.Application.Services.Implementations;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Data;
using MVCProyectoNFT.Infraestructure.Repository.Implementations;
using MVCProyectoNFT.Infraestructure.Repository.Interface;
using MVCProyectoNFT.Web;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfig>(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure D.I.
builder.Services.AddTransient<IRepositoryPais, RepositoryPais>();
builder.Services.AddTransient<IServicePais, ServicePais>();
builder.Services.AddTransient<IServiceNft, ServiceNft>();
builder.Services.AddTransient<IRepositoryNFT, RepositoryNFT>();
builder.Services.AddTransient<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddTransient<IServiceCliente, ServiceCliente>();
builder.Services.AddTransient<IRepositoryTipoTarjeta, RepositoryTipoTarjeta>();
builder.Services.AddTransient<IServiceTipoTarjeta,  ServiceTipoTarjeta>();
builder.Services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
builder.Services.AddTransient<IServiceUsuario, ServiceUsuario>();
builder.Services.AddTransient<IRepositoryFactura, RepositoryFactura>();
builder.Services.AddTransient<IServiceFactura, ServiceFactura>();
builder.Services.AddTransient<IServiceReporte, ServiceReporte>();
builder.Services.AddTransient<IRepositoryClienteNFT, RepositoryClienteNFT>();
builder.Services.AddTransient<IServiceClienteNFT, ServiceClienteNFT>();

// config Automapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<PaisProfile>();
    config.AddProfile<NftProfile>();
    config.AddProfile<ClienteProfile>();
    config.AddProfile<TipoTarjetaProfile>();
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<FacturaProfile>();
});

// Config Connection to SQLServer DataBase
builder.Services.AddDbContext<ProyectoNFTContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDataBase"));

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});




// Logger. P.E. Verbose = it shows SQl Statement
var logger = new LoggerConfiguration()
                    // .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(LogEventLevel.Verbose)
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"Logs\Info-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"Logs\Debug-.log", shared: true, encoding: System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"Logs\Warning-.log", shared: true, encoding: System.Text.Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.ASCII, rollingInterval: RollingInterval.Day))
                    .CreateLogger();

builder.Host.UseSerilog(logger);

var app = builder.Build();

/// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Error control Middleware
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Activate Antiforgery DEBE COLOCARSE ACA!
app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
