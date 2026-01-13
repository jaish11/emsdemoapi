using emsdemoapi.Data;
using emsdemoapi.Data.Excel;
using emsdemoapi.Data.Interfaces;
using emsdemoapi.Data.Services;
using log4net;
using log4net.Config;
//this is for JWT authentication
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // ✅ Add this at the top of your file if not already
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//this is implementation of in-memory caching
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "emsdemoapi_";
});
builder.Services.AddScoped<RedisCacheService>();


/*//for catching the responces in cache
 * 1.this line in program.cs is needed Controller usage (useful for simple GETs):
//builder.Services.AddResponseCaching();*/
/*
 * Add Output Caching it only for .NET 8 and above 
 * inthis it will not show in responce headers but it will work you can check in logger
//builder.Services.AddOutputCache(options =>
//{
//    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromSeconds(120)));
//});
*/
/* this is for log4net configuration and in bin folder
 * <file type="log4net.Util.PatternString" value="Logs/log-%date{yyyyMMdd}.txt" />*/
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();


//use logger Logs in current folder
//GlobalContext.Properties["LogPath"] = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

//var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
//XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

//builder.Logging.ClearProviders();
//builder.Logging.AddLog4Net("log4net.config");

//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

#region Swagger with JWT Authentication

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "emsdemoapi",
        Version = "v1",
        Description = "EMS Demo API with JWT Authentication"
    });

    // 🔐 Add JWT Authentication Definition
    #region UI Enhancements
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });
    #endregion UI Enhancements

    // 🔐 Apply JWT Requirement Globally
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

#endregion Swagger with JWT Authentication
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped(typeof(IGeneric<>), typeof(GenericService<>));
builder.Services.AddScoped<IBranch, BranchService>();
builder.Services.AddScoped<ILanguage, LanguageService>(); 
builder.Services.AddScoped<ICountry,CountryService>();
builder.Services.AddScoped<IState,StateService>();
builder.Services.AddScoped<IDistrict,DistrictService>();
builder.Services.AddScoped<ICustomer,CustomerService>();
builder.Services.AddScoped<IBook,BookService>();
builder.Services.AddScoped<IEmployee,EmployeeService>();
////this is for excel
//builder.Services.AddScoped<BranchExcelService>();
//builder.Services.AddScoped<DistrictExcelService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseResponseCaching();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
/*//app.UseResponseCaching();
app.UseOutputCache();*/
app.MapControllers();
app.Run();