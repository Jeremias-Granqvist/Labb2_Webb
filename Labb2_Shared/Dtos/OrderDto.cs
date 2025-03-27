using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateOnly DateOfOrder { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
