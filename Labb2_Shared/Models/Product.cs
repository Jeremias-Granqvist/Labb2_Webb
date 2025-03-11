namespace Labb2_Shared.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public Category ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public string Status { get; set; }
    }
}
