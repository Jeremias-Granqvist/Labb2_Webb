using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos
{
    public class CustomerWithDetailsDto
    {
        public CustomerDto Customer { get; set; }
        public AdressDto Adress { get; set; }
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }

}
