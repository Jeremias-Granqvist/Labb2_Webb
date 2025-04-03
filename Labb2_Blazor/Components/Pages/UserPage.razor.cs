using Labb2_Infrastructure.Authentication.Services;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Labb2_Blazor.Components.Pages
{
    public partial class UserPage
    {
        private ApplicationUser user;
        private List<Order> orders;
        private bool isInitialized = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authToken = await localStorage.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(authToken))
                {
                    // Redirect to login if the user is not authenticated
                    navigationManager.NavigateTo("/login");
                    return;
                }

                // Fetch user data and orders after checking the auth token
                await LoadUserData();
                isInitialized = true; // Set initialization flag to true
                StateHasChanged(); // Trigger a re-render to show the content
            }
        }

        private async Task LoadUserData()
        {
            try
            {
                var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
                var currentUser = authState.User;

                if (currentUser.Identity.IsAuthenticated)
                {
                    var userName = currentUser.Identity.Name;
                    Console.WriteLine($"Logged in user: {userName}");

                    await FetchUserData(currentUser);
                }
                else
                {
                    navigationManager.NavigateTo("/login");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user data: {ex.Message}");
            }
        }

        private async Task FetchUserData(ClaimsPrincipal currentUser)
        {
            try
            {
                var userName = currentUser.Identity.Name;
                user = await customerService.GetUserByEmailAsync(userName);
              //  user = await customerService.GetUsersWithOrdersAsync(user.UserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user data: {ex.Message}");
            }
        }
    }
}