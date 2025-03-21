using Labb2_Blazor.Dto;
using Labb2_Blazor.Models;
using Labb2_Blazor.State;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Components;

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
            var response = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product");
            if (response != null)
            {
                allProducts = response;
                filteredProducts = response;
            }
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

        private string GetCategoryName(int categoryId)
        {
            var category = allCategories.FirstOrDefault(c => c.Id == categoryId);
            return category?.Name ?? "Unknown Category";
        }
    }
}