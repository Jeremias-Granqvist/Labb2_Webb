using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos
{
    public class ApplicationUserWithDetailsDTO
    {
        public ApplicationUserDTO Customer { get; set; }
        public AdressDto Adress { get; set; }
        public List<OrderWithDetailsDto> Orders { get; set; } = new List<OrderWithDetailsDto>();
    }

}