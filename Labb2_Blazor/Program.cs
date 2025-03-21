using Labb2_Blazor.Components;
using Labb2_Blazor.Dto;
using Labb2_Blazor.State;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSingleton<AppState>();

builder.Services.AddHttpClient("Api", client =>
{
     client.BaseAddress = new Uri("https://localhost:5189");
    //var BaseUrl = builder.Configuration["BaseUrl"];
    //client.BaseAddress = new Uri(BaseUrl); 
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; //    scope.ServiceProvider används för att hämta tjänster inom det nya scopet.
    var appState = services.GetRequiredService<AppState>(); //services.GetRequiredService<AppState>() hämtar AppState-instansen från DI.
    var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("Api");

    try
    {
        appState.Categories =
            await httpClient.GetFromJsonAsync<List<CategoryDtoFrontend>>("api/categories")
            ?? new List<CategoryDtoFrontend>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fel vid inläsning av kategorier: {ex.Message}");
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
