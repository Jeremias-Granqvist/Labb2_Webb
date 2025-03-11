using System;
using System.Collections.Generic;

namespace Labb2_API.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int? Price { get; set; }

    public int? ProductCategoryId { get; set; }

    public string? Status { get; set; }

    public virtual Category? ProductCategory { get; set; }
}
