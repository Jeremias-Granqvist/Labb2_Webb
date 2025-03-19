using Labb2_Blazor.Models;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AddProduct
    {
        private ProductDtoFrontend? Product { get; set; }

        protected bool isProductSaved { get; set; }
    }
}