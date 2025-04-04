using Labb2_Blazor.Dto;
using Labb2_Blazor.Models;
using Labb2_Blazor.State;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Labb2_Infrastructure.Authentication.States;
using Labb2_Infrastructure.Authentication.Repos;
using Labb2_Infrastructure.Authentication.Services;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default;
        private HttpClient? _httpClient;
        [Inject]
        public AppState appState { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public AccountService accountService { get; set; }

        [SupplyParameterFromForm]
        private ProductDto? Product { get; set; }

        public List<CategoryDtoFrontend> Categories { get; set; } = new();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;
        protected bool isProductSaved { get; set; }
        private bool _IsAuthorized = false;


        protected override async Task OnInitializedAsync()
        {
            if (Constants.JWTToken == "") 
            {
                NavManager.NavigateTo("/login");
                return;
            }
            else
            {
                _IsAuthorized = true;
                _httpClient = HttpClientFactory.CreateClient("Api");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);
                isProductSaved = false;
                Product ??= new ProductDto();

                try
                {
                    if (appState.Categories.Count == 0)
                    {
                        await appState.InitializeAsync(_httpClient);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.Error.WriteLine($"Error fetching categories: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                }

                Categories = appState.Categories;
            }

                

        }

        private void InvalidInput()
        {
            statusClass = "alert-danger";
            message = "Something is incorrect, please doublecheck your input";
            StateHasChanged();
        }
        
        public async Task ValidInput()
        {
            var response = await _productService.CreateProductAsync(Product);
            //var response = await _httpClient.PostAsJsonAsync("api/product", Product);

            statusClass = "alert-success";
            message = "Product added to database";
            isProductSaved = true;
            }
        }


    }
