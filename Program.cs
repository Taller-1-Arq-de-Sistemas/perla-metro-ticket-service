using perla_metro_tickets_service.src.Data;
using perla_metro_tickets_service.src.Helpers;
using perla_metro_tickets_service.src.Interfaces;
using perla_metro_tickets_service.src.Repositories;
using perla_metro_tickets_service.src.Services;

var builder = WebApplication.CreateBuilder(args);

//configuración de servicios a utilizar

builder.Services.AddSingleton<MongoContext>();

//Repositorios y servicios
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMapperService, MapperService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();