using System.Text.Json.Serialization;

namespace Labb2_Shared.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        [JsonIgnore]
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
    //public Category ProductCategory { get; set; }
    //public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public class Reviews
    {
        public int Rating { get; set; }
        public string Review { get; set; }
    }


}
