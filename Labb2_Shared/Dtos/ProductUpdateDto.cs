using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Dtos
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }


    }
}
