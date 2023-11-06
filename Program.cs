using Microsoft.EntityFrameworkCore;
using NPWalks.API.Data;
using NPWalks.API.Mappings;
using NPWalks.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStrings = builder.Configuration.GetConnectionString("NPWalksConnectionString");

builder.Services.AddDbContext<NPWalksDBContext>(options =>
options.UseNpgsql(connectionStrings)
 );

// Inject Interface with their imlementation
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();


// Automapper Injection

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
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
