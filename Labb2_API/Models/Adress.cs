using System;
using System.Collections.Generic;

namespace Labb2_API.Models;

public partial class Adress
{
    public int AdressId { get; set; }

    public string? StreetName { get; set; }

    public string? ZipCode { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
