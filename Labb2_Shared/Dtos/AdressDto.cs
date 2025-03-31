using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos;

public class AdressDto
{
    public int AdressId { get; set; }
    public string? StreetName { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public virtual ICollection<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
}
