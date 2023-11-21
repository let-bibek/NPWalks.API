using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPWalks.API.Data;
using NPWalks.API.Mappings;
using NPWalks.API.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using NPWalks.API.Middlewares;
// using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Serilog 
// var logger =new LoggerConfiguration();

builder.Services.AddControllers();


// File upload ko laagi use hunchha
builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger 
builder.Services.AddSwaggerGen(options =>
{
    // Add authorization in swagger
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NP Walks API", Version = "v1" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference=new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="Oauth2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In=ParameterLocation.Header

            },
            new List<string>()
        }
    });
});

var connectionStrings = builder.Configuration.GetConnectionString("NPWalksConnectionString");

builder.Services.AddDbContext<NPWalksDBContext>(options =>
options.UseNpgsql(connectionStrings)
 );

// AuthDB connections
builder.Services.AddDbContext<NPWalksAuthDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("NPWalksAuthConnectionString"));
});

// Inject Interface with their implementation
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();


// Automapper Injection
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Setting Identity
builder.Services.AddIdentityCore<IdentityUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NPWalks")
.AddEntityFrameworkStores<NPWalksAuthDBContext>()
.AddDefaultTokenProviders();

// Setting options for identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


// To show static files like images, css through urls
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"

});


app.MapControllers();


app.Run();
