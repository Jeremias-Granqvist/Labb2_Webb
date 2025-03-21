using Labb2_Blazor.Dto;
using Labb2_Blazor.Models;

namespace Labb2_Blazor.State;

public class AppState
{
    public List<CategoryDtoFrontend> Categories { get; set; } = new List<CategoryDtoFrontend>();
    public ProductDtoFrontend SelectedProduct { get; set; }

    public async Task InitializeAsync(HttpClient http)
    {
        if (Categories.Count == 0)
        {
            var FetchedCategories = await http.GetFromJsonAsync<List<CategoryDtoFrontend>>("api/categories");
                Categories = FetchedCategories ?? new List<CategoryDtoFrontend>();
        }
    }
}
