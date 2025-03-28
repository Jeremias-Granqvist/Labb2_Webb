using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Labb2_Shared.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; }
}
