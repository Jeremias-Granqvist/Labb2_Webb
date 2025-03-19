using Labb2_Blazor.Models;

namespace Labb2_Blazor.Dto
{
    public class UpdateProductDtoFrontend
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }

        public UpdateProductDtoFrontend(ProductDtoFrontend product)
        {
            if (product != null)
            {
                Name = product.Name;
                Description = product.Description;
                Price = product.Price;
                Status = product.Status;
                CategoryId = product.CategoryId;
            }
        }
        public UpdateProductDtoFrontend(){ }
    }
}
