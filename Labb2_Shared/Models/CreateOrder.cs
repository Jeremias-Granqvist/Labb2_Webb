using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Models
{
    public class CreateOrder
    {
        public string customerMail { get; set; }
        public List<int> productIds { get; set; } = new List<int>();
    }
}
