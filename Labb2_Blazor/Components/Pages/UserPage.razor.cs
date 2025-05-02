using Labb2_Infrastructure.Authentication.Services;
using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Labb2_Blazor.Components.Pages
{
    public partial class UserPage
    {
        private ApplicationUserDTO user;
        private List<OrderDto> orders;
        private bool isInitialized = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authToken = await localStorage.GetItemAsync<string>("authToken");
                if (string.IsNullOrEmpty(authToken))
                {
                    navigationManager.NavigateTo("/login");
                    return;
                }

                // Fetch user data and orders after checking the auth token
                await LoadUserData();
                if (user.Adress == null)
                {
                    user.Adress = new Adress();
                }
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
                var tempOrders = await orderService.GetAllOrdersAsync();
                orders = tempOrders.Where(o => o.UserID == user.UserId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user data: {ex.Message}");
            }
        }

        private async void UpdateCustomer()
        {
            var response = await customerService.UpdateUserAsync(user.UserId, user);
        }
    }
}