namespace Labb2_Shared.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? UserID { get; set; }
        public DateOnly DateOfOrder { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
