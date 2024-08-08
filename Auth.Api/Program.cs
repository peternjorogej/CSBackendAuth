
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Auth.Api.Services;
using Auth.Api.Controllers;

namespace Auth.Api;

public static class Program
{
    private const string MyAllowSpecificOrigins = "*";

    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer(); // Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

            // Define the BearerAuth scheme
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In           = ParameterLocation.Header,
                Description  = "Please insert JWT with Bearer into field",
                Name         = "Authorization",
                Type         = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme       = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>() // new string[] { }
            }});
        });

        builder.Services.AddDbContext<SqliteDatabaseService>()
                        .AddDbContext<UserDatabaseService>();
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<UserDatabaseService>()
                        .AddDefaultTokenProviders();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                name: MyAllowSpecificOrigins,
                configurePolicy: policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                }
            );
        });
        
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer   = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer      = MyJwtInfo.Issuer,
                    ValidAudience    = MyJwtInfo.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MyJwtInfo.Key))
                };
            });
        builder.Services.AddAuthorization();


        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();

        app.Run();
    }
}
