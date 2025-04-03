using Labb2_Blazor.Components;
using Labb2_Blazor.Dto;
using Labb2_Blazor.State;
using Labb2_Infrastructure.Authentication.Services;
using Labb2_Infrastructure.Authentication.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<AppState>();

builder.Services.AddHttpClient("Api", client =>
{
     client.BaseAddress = new Uri("https://localhost:7002");

});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";
})
.AddJwtBearer("Bearer", options =>
{
    options.Authority = "";
    options.Audience = "my-api";
    options.RequireHttpsMetadata = true;
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<IJSRuntime, JSRuntime>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; //    scope.ServiceProvider används för att hämta tjänster inom det nya scopet.
     var appState = services.GetRequiredService<AppState>(); //services.GetRequiredService<AppState>() hämtar AppState-instansen från DI.
    var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("Api");

    try
    {
        var response = await httpClient.GetAsync("api/categories");
        if (response.IsSuccessStatusCode)
        {
        appState.Categories =
            await httpClient.GetFromJsonAsync<List<CategoryDtoFrontend>>("api/categories")
            ?? new List<CategoryDtoFrontend>();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading categories: {ex.Message}");
        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
        }
    }
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
