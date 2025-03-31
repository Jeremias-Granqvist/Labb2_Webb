using Labb2_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class OrderItemsController
    {
        private readonly IOrderitemRepository _repository;
        public OrderItemsController()
        {
            
        }
    }
}
