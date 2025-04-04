using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Labb2_Shared.Models
{
    public partial class Order
    {
        [Column("OrderId")]
        public int OrderId { get; set; }
        public int? UserID { get; set; }
        public DateOnly DateOfOrder { get; set; }

        public virtual ApplicationUser User { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        [JsonIgnore]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }


    }
    //public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
