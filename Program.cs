using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NightPhotoBackend.Models;
using NightPhotoBackend.Helpers;
using NightPhotoBackend.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NightPhotoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFolderCreator, FolderCreator>();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IResponseCookies, ResponseCookies>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("reactfrontend", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:3000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});

var secretKey = builder.Configuration["AppSettings:Secret"];



var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "NightPhotoServer",
    
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
};

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("access_token"))
            {
                context.Token = context.Request.Cookies["access_token"];
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment()) // by default enabled only for dev.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("reactfrontend"); // Move UseCors before UseAuthentication and UseAuthorization

app.UseAuthentication(); // Ensure UseAuthentication is called before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();

