using Blazored.LocalStorage;
using Blazored.SessionStorage;
using HRIS_UI.Services;
using Microsoft.Fast.Components.FluentUI;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages and Server-Side Blazor
builder.Services.AddRazorPages(); // required for _Host.cshtml
builder.Services.AddServerSideBlazor();
// Add services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddHttpClient();

builder.Services.AddFluentUIComponents(); // IDialogService
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<UserState>();

builder.Services.AddAuthorizationCore();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
    });
builder.Services.AddFluentUIComponents();
var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Map Blazor Hub
app.MapBlazorHub();

// Map fallback to _Host.cshtml in Pages folder
app.MapFallbackToPage("/_Host");

app.Run();
