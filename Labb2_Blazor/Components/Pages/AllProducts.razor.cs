using Labb2_Blazor.Dto;
using Labb2_Blazor.Models;
using Labb2_Blazor.State;
using Labb2_Infrastructure.Services;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AllProducts
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default;
        private HttpClient? _httpClient;
        [Inject]
        public AppState appState { get; set; }

        private string searchQuery = string.Empty;
        private List<ProductDto> allProducts = new List<ProductDto>();
        private List<ProductDto> filteredProducts = new List<ProductDto>();
        private List<CategoryDto> allCategories = new List<CategoryDto>();

        public List<int> cart = new List<int>();

        private bool IsOrderButton = false;
        protected string message = string.Empty;
        protected string statusClass = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("Api");
            await FetchCategories();
            await FetchProducts();
        }

        private async Task FetchCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
            if (response != null)
            {
                allCategories = response;
            }
        }

        private async Task FetchProducts() 
        {

            var response = ProductService.GetAllProductsAsync();
            if (response != null)
            {
                allProducts = await response;
                filteredProducts = allProducts;
            }
        
            //try
            //{
            //var response = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product");
            //if (response != null)
            //{
            //    allProducts = response;
            //    filteredProducts = response;
            //}

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"ERROR ::: {ex.Message}");
            //    throw;
            //}
        }

        private void SearchProducts()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredProducts = allProducts;
            }
            else
            {
                filteredProducts = allProducts
                    .Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
                StateHasChanged();
        }

        private List<int>PlaceInCart(int productID)
        {
            IsOrderButton = true;
            cart.Add(productID);
            StateHasChanged();
            return cart;
        }

        private string GetCategoryName(int categoryId)
        {
            var category = allCategories.FirstOrDefault(c => c.Id == categoryId);
            return category?.Name ?? "Unknown Category";
        }

        private async void PlaceOrder()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userEmail = authenticationState.User.Identity.Name;
            bool result = false;
            if (userEmail is null || userEmail == "")
            {
                await JSRuntime.InvokeVoidAsync("alert", "Please log in before placing an order.");

            }
            else
            {

                result = await OrderService.PlaceOrderAsync(userEmail, cart.ToList());
            }
            if (result)
            {
                await JSRuntime.InvokeVoidAsync("alert", "Order placed.");
                cart.Clear();
                StateHasChanged();
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alert", "something went wrong, please try again later.");
            }
        }
    }
}