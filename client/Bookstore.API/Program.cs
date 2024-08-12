//using Microsoft.AspNetCore.StaticFiles;
using Bookstore.API;
using Bookstore.API.DbContexts;
using Bookstore.API.Repositories;
using Bookstore.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/bookstore.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();
    
// Add services to the container.

builder.Services.AddControllers(options=>{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails();
//builder.Services.AddProblemDetails(options =>
//{
//    options.CustomizeProblemDetails = ctx =>
//    {
//        ctx.ProblemDetails.Extensions.Add("additionalInfo", "Addtional info example");
//        ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
//    };
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<BookstoreDataStore>();

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.AddControllersWithViews()
//           .AddViewLocalization()
//           .AddDataAnnotationsLocalization();

builder.Services.AddDbContext<BookstoreInfoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthorsInfoRepository, AuthorsInfoRepositoryService>();
builder.Services.AddScoped<IGenresInfoRepository, GenresInfoRepositoryService>();
builder.Services.AddScoped<IBooksInfoRepository, BooksInfoRepositoryService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
               Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    );

// adding authentication option in swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreAPI", Version = "v1" });

    // Configure Swagger to use Bearer token authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please Paste your generated Token here WITHOUT Double Quotes",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

///

//adding cors for UI integration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
      builder =>
      {
          builder.WithOrigins("http://localhost:3000") // URL of your React app
         .AllowAnyHeader()
         .AllowAnyMethod();
          // builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
});
///

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowLocalhost3000"); // Apply the CORS policy

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
