using Labb2_Infrastructure.Authentication.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.NetworkInformation;
using Blazored.LocalStorage;

namespace Labb2_Blazor.Components.Pages
{
    public partial class Logout
    {
        
        protected override async Task OnInitializedAsync()
        {
            LogoutFromSite();
        }
        private async Task LogoutFromSite()
        {
            await localStorage.RemoveItemAsync("authToken");
            await accountService.Logout();
            Console.WriteLine("Logout button clicked, token removed and logout called.");
            navigationManager.NavigateTo("/");

        }
    }
}