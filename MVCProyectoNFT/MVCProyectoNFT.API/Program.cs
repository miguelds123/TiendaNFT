using Microsoft.EntityFrameworkCore;
using MVCProyectoNFT.Application.Profiles;
using MVCProyectoNFT.Application.Services.Implementations;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Data;
using MVCProyectoNFT.Infraestructure.Repository.Implementations;
using MVCProyectoNFT.Infraestructure.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// Configure D.I.
builder.Services.AddTransient<IRepositoryPais, RepositoryPais>();
builder.Services.AddTransient<IServicePais, ServicePais>();
builder.Services.AddTransient<IServiceNft, ServiceNft>();
builder.Services.AddTransient<IRepositoryNFT, RepositoryNFT>();
builder.Services.AddTransient<IRepositoryCliente, RepositoryCliente>();
builder.Services.AddTransient<IServiceCliente, ServiceCliente>();
builder.Services.AddTransient<IRepositoryTipoTarjeta, RepositoryTipoTarjeta>();
builder.Services.AddTransient<IServiceTipoTarjeta, ServiceTipoTarjeta>();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
