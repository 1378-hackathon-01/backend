using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;
using Thon.App;
using Thon.Web.Authorization;
using Thon.Web.Conventions;
using Thon.Web.Converters;
using Thon.Web.Helpers;
using Thon.Web.Middlewares;
using Thon.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var thonConfigGenerator = new ThonConfigurationGenerator(configuration);
var thonConfig = thonConfigGenerator.Get();
builder.Services.AddThonServices(thonConfig);

var authOptions = new AuthOptions();
builder.Services.AddSingleton(authOptions);
builder.Services.AddSingleton<AuthHelper>();

builder
    .Services
    .AddAuthorizationBuilder()
    .AddPolicy(
        AuthPolicies.Admin,
        policy => policy
            .RequireClaim(AuthTokenClaim.Role, AuthTokenRole.Admin)
            .RequireClaim(AuthTokenClaim.SessionId))
    .AddPolicy(
        AuthPolicies.Institution,
        policy => policy
            .RequireClaim(AuthTokenClaim.Role, AuthTokenRole.Institution)
            .RequireClaim(AuthTokenClaim.SessionId))
    .AddPolicy(
        AuthPolicies.Api,
        policy => policy
            .RequireApiToken());
    
            

builder
    .Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,

            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
        };

        options.TokenHandlers.Add(new BearerTokenHandler());
    });

builder
    .Services
    .AddControllers(options =>
    { 
        options.Conventions.Add(new FolderRouteConvention()); 
    })
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        options.JsonSerializerOptions.Converters.Add(new AdminAccessLevelConverter());

    });

builder.Services.AddSingleton<ApiTokenCache>();

builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();
app.MapControllers();
app.Run();
