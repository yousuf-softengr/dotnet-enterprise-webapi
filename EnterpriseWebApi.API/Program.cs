using System.Text;
using EnterpriseWebApi.API.Auth;
using EnterpriseWebApi.API.Middleware;
using EnterpriseWebApi.Application;
using EnterpriseWebApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//
// 1 Configuration bindings
//
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

//
// 2️ JWT services
//
builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt.Key))
        };
    });

builder.Services.AddAuthorization();

//
// 3️ Controllers
//
builder.Services.AddControllers();

//
// 4️ Swagger + OpenAPI + JWT
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //  REQUIRED OpenAPI definition
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Enterprise Web API",
        Version = "v1",
        Description = "Enterprise-grade .NET Web API with JWT Authentication"
    });

    //  JWT Bearer configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token as: Bearer {your JWT token}"
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

//
// 5️ Application & Infrastructure layers
//
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

//
// 6️ Development-only Swagger
//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Enterprise Web API v1"
        );

        //  VERY IMPORTANT
        c.RoutePrefix = "swagger";
    });
}


//
// 7️ Middleware pipeline (ORDER MATTERS)
//
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

//
// 8️ Map endpoints
//
app.MapControllers();

app.Run();
