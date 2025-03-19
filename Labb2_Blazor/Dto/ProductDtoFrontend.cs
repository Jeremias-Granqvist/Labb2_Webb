namespace Labb2_Blazor.Models
{
    public class ProductDtoFrontend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
    }
}
