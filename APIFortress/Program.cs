using ApiFortress.Infrastructure.Config;
using ApiFortress.Infrastructure.Data;
using ApiFortress.Infrastructure.Middleware;
using ApiFortress.Infrastructure.Providers;
using ApiFortress.Application.Interfaces;
using ApiFortress.Application.Services;
using ApiFortress.Infrastructure.Repositories;
using ApiFortress.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiFortressDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(appSettings.JwtSecret);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = appSettings.JwtIssuer,
        ValidateAudience = true,
        ValidAudience = appSettings.JwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<IPermissionRepository, PermissionRepository>();

builder.Services.AddSingleton<JWTTokenProvider>(sp => new JWTTokenProvider(
    appSettings.JwtSecret,
    appSettings.JwtIssuer,
    appSettings.JwtAudience,
    appSettings.JwtExpirationInMinutes));
builder.Services.AddSingleton<APIKeyProvider>();
builder.Services.AddSingleton<RateLimiter>(sp =>
    new RateLimiter(appSettings.RateLimit, TimeSpan.FromSeconds(appSettings.RateLimitIntervalInSeconds)));
builder.Services.AddSingleton<IPBlocker>();
builder.Services.AddSingleton<EncryptionProvider>(sp => new EncryptionProvider(
    appSettings.EncryptionKey, appSettings.EncryptionIV));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<IPBlockMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
