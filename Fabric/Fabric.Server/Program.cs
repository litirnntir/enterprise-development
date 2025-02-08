using AutoMapper;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;
using Fabrics.Server;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Register repositories
builder.Services.AddScoped<IRepository<Factory>, FabricRepository>();
builder.Services.AddScoped<IRepository<Provider>, ProviderRepository>();
builder.Services.AddScoped<IRepository<Shipment>, ShipmentRepository>();

// Configure DbContext
builder.Services.AddDbContextFactory<FabricsDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Factory")!));

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
