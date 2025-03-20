using Labb2_Blazor.Dto;
using Labb2_Blazor.Models;
using Labb2_Blazor.State;
using Microsoft.AspNetCore.Components;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default;
        private HttpClient? _httpClient;
        [Inject]
        public AppState appState { get; set; }

        [SupplyParameterFromForm]
        private ProductDtoFrontend? Product { get; set; }

        public List<CategoryDtoFrontend> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductSaved { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("Api");
            isProductSaved = false;
            Product ??= new();

            if (appState.Categories.Count == 0)
            {
                await appState.InitializeAsync(_httpClient);
            }
            Categories = appState.Categories;
        }

        private void InvalidInput()
        {
            statusClass = "alert-danger";
            message = "Something is incorrect, please doublecheck your input";
            StateHasChanged();
        }
        
        private async Task ValidInput()
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", Product);
            statusClass = "alert-success";
            message = "Product added to database";
            isProductSaved = true;
        }


    }
}