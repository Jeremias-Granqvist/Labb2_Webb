using Labb2_Infrastructure.Repositories;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{


    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;


    public OrderController(ICustomerRepository customerRepository,
                           IProductRepository productRepository,
                           IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Get a list of all orders
    /// </summary>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    //GET (hämta med API)
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
    }

    /// <summary>
    /// sends information about order from OrderID
    /// </summary>
    /// <param name="id">The ID of the order to retrieve. This ID is used to fetch the order details from the database, including customer and product information.</param>
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderWithCustomerAndItems(int id)
    {
        var order = await _orderRepository.GetOrderWithCustomerAndItemsAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }


    //POST (skapa med API)

    /// <summary>
    /// Creates a new order and sends to DB
    /// </summary>
    /// <param name="createOrder">The `CreateOrder` object containing order details such as customer email and selected product IDs.</param
    /// <returns>
    ///     HTTP 200 OK: The request was successful and the response contains the requested data.
    ///     HTTP 201 Created: The request was successful and a new resource has been created.
    ///     HTTP 400 Bad Request: The request was invalid. Please check the request body or query parameters.
    ///     HTTP 404 Not Found: The requested resource was not found on the server.
    ///     HTTP 500 Internal Server Error: An unexpected error occurred on the server. Please try again later.
    /// </returns>    
    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrderAsync(CreateOrder createOrder)
    {
        var customer = await _customerRepository.GetUserFromEmailAsync(createOrder.customerMail);
        if (customer == null) return NotFound("Customer not found.");
        var products = await _productRepository.GetAllProductsAsync();
        var selectedProducts = products.Where(p => createOrder.productIds.Contains(p.Id)).ToList();

        if (!selectedProducts.Any()) return BadRequest("No valid products selected.");

        var order = new Order
        {
            UserID = customer.UserId,
            User = customer,
            DateOfOrder = DateOnly.FromDateTime(DateTime.Now),
            Products = selectedProducts
        };

        await _orderRepository.CreateOrderAsync(order);

        return Ok();
    }   
}

