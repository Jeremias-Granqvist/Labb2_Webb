using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Labb2_Shared.Models;

public partial class Adress
{
    public int AdressId { get; set; }

    public string? StreetName { get; set; }

    public string? ZipCode { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
