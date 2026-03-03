using Emgu.CV.Face;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Handlers;
using HRIS.Application.Features.Schedule.Commands;
using HRIS.Application.Features.TimeEntries.Queries;
using HRIS.Infrastructure;
using HRIS.Infrastructure.Persistence;
using HRIS.Infrastructure.Services;
using MediatR;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Load configuration -------------------
// Add your custom JSON file to the existing ConfigurationManager
var configFile = @"C:\app\hris_v2\appconfig.json";
builder.Configuration
    .AddJsonFile(configFile, optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = builder.Configuration; // safe to use in services

// ------------------- Add Infrastructure / Services -------------------
builder.Services.AddInfrastructure(configuration);

// Register FaceRecognitionService
builder.Services.AddScoped<IFaceRecognitionService, OpenCvFaceRecognitionService>();

// ------------------- Add MediatR -------------------
// Register handlers from the assembly where IdentifyFaceHandler exists

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(IdentifyFaceHandler).Assembly,
    typeof(GetActiveTimeEntryQuery).Assembly,
    typeof(CreateWeeklyScheduleCommand).Assembly
));
// ------------------- Controllers -------------------
builder.Services.AddControllers();

// ------------------- Build App -------------------
var app = builder.Build();

// ------------------- Swagger -------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRIS API 1.0.0");
        c.DocExpansion(DocExpansion.None);
    });
}

// ------------------- Middleware -------------------
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// ------------------- Map Controllers -------------------
app.MapControllers();

// ------------------- Run App -------------------
app.Run();
