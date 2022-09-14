using ApiEvents.Core.Interfaces;
using ApiEvents.Core.Services;
using ApiEvents.Filters;
using ApiEvents.Infra;
using ApiEvents.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnectionDataBase, ConnectionDataBase>();
builder.Services.AddScoped <ICityEventService,CityEventService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();

builder.Services.AddMvc(options =>
    {
        options.Filters.Add<ExceptionGlobalFilter>();
    }
);

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
