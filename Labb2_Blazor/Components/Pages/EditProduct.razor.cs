using Labb2_Blazor.State;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Cryptography;

namespace Labb2_Blazor.Components.Pages;

public partial class EditProduct
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

    private ProductDto productToEdit;
    protected bool isEditing = false;

    protected bool isProductSaved { get; set; }

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

    private void OnEditClick(ProductDto product)
    {
        productToEdit = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId
        };
        isEditing = true;
    }

    private void CancelEdit()
    {
        isEditing = false;
        productToEdit = null;
    }

    private async Task UpdateProduct()
    {
        var response = await _httpClient.PutAsJsonAsync($"api/product/{productToEdit.Id}", productToEdit);
        Console.WriteLine($"id is: {productToEdit.Id}");
        
        CheckResult(response);
    }

    private async Task DeleteProduct()
    {
        var response = await _httpClient.DeleteFromJsonAsync<HttpResponseMessage>($"api/product/{productToEdit.Id}");
        CheckResult(response);

    }

    private async void  CheckResult(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            await FetchProducts();
            await FetchCategories();
            isEditing = false;
            productToEdit = null;
        }
        else
        {
            Console.WriteLine("Failed to update product");
        }
            StateHasChanged();
            
    }
}