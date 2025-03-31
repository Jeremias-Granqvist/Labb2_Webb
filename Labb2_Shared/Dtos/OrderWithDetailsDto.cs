using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos
{
    public class OrderWithDetailsDto
    {
        public OrderDto Order { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

    }
}
