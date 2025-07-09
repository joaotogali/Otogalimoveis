using Otogalimoveis.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Otogalimoveis.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddDbContext<OtogaliDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Data layer dependencies
builder.Services.AddScoped<Otogalimoveis.Domain.Data.IImovelData, Otogalimoveis.Infrastructure.Data.ImovelData>();
builder.Services.AddScoped<Otogalimoveis.Domain.Data.ILocatarioData, Otogalimoveis.Infrastructure.Data.LocatarioData>();
builder.Services.AddScoped<Otogalimoveis.Domain.Data.IAluguelData, Otogalimoveis.Infrastructure.Data.AluguelData>();

// Register Application Services
builder.Services.AddScoped<Otogalimoveis.Application.Services.IImovelService, Otogalimoveis.Application.Services.ImovelService>();
builder.Services.AddScoped<Otogalimoveis.Application.Services.IAluguelService, Otogalimoveis.Application.Services.AluguelService>();
builder.Services.AddScoped<Otogalimoveis.Application.Services.ILocatarioService, Otogalimoveis.Application.Services.LocatarioService>();

// Register DTO Services
builder.Services.AddScoped<Otogalimoveis.Application.Services.IImovelServiceDTO, Otogalimoveis.Application.Services.ImovelServiceDTO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
