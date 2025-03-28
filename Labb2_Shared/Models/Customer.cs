using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Labb2_Shared.Models;

public partial class Customer
{
    public int CustomerId { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string PhoneNo { get; set; }
    public int? AdressId { get; set; }

    public virtual Adress? Adress { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); 

}
