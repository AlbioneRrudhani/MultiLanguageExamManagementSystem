using AutoMapper;
using LifeEcommerce.Helpers;
using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Data;
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Services.IServices;
using MultiLanguageExamManagementSystem.Services;
using Serilog;
using MultiLanguageExamManagementSystem.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using MultiLanguageExamManagementSystem.Helpers.Models;


var builder = WebApplication.CreateBuilder(args);

var mapperConfiguration = new MapperConfiguration(
                        mc => mc.AddProfile(new AutoMapperConfigurations()));

IMapper mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.Configure<Auth0Settings>(builder.Configuration.GetSection("Auth0"));


// Register HttpClient
builder.Services.AddHttpClient<TranslationService>(); 

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IAdminExamService, AdminExamService>();
builder.Services.AddScoped<ICultureService, CultureService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<IAuth0Service, Auth0Service>();

builder.Services.AddScoped<IClaimsPrincipalAccessor, ClaimsPrincipalAccessor>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Professor", policy =>
                      policy.RequireClaim("permissions", "manage:exams"));
});


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", 
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
    c.OperationFilter<AuthorizeCheckOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<CultureMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
