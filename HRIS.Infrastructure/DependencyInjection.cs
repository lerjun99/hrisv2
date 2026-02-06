using HRIS.Application.Common.Interfaces;
using HRIS.Infrastructure.Persistence;
using HRIS.Infrastructure.Persistence.Repositories;
using HRIS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace HRIS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration? configuration = null)
        {
            // If configuration not provided, load it manually
            if (configuration == null)
            {
                configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("app/hris_v2/appconfig.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
            }

            // ------------------- EF Core -------------------
            services.AddDbContext<HrisDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HrisV2_ConnectionString")));
            services.AddScoped<IHrisDbContext>(sp => sp.GetRequiredService<HrisDbContext>());
            services.AddScoped<IJwtTokenService, JwtAuthenticationManager>();
            services.AddScoped<ICryptography, Cryptography>();
            services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();

            // ------------------- Controllers + JSON Options -------------------
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            // ------------------- Swagger -------------------
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0", // ✅ Must be semantic version
                    Title = "HRIS API",
                    Description = "HRIS API for Blazor / .NET Core"
                });

                c.UseInlineDefinitionsForEnums();
                c.CustomSchemaIds(type => type.FullName);

                // JWT Security Definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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

                c.OperationFilter<FileUploadOperationFilter>();
            });

            // ------------------- Authentication -------------------
            services.AddAuthentication("Basic")
                .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("Basic", null);

            // ------------------- Authorization -------------------
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKey", authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });
            });

            return services;
        }
    }
}
