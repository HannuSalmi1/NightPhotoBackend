using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NightPhotoBackend.Entities;
using NightPhotoBackend.Helpers;
using NightPhotoBackend.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyHeader();
            policy.WithOrigins("http://localhost:3000");
        });
});
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


// Add services to the container.
builder.Services.AddDbContext<NightPhotoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFolderCreator, FolderCreator>();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IResponseCookies,  ResponseCookies>();

var secretKey = builder.Configuration["AppSettings:Secret"];
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "NightPhotoServer",
    ValidAudience = "YourValidAudience",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
};

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment()) // by default enabled only for dev.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
