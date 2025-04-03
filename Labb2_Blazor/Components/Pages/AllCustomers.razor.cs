using Labb2_Blazor.State;
using Labb2_Infrastructure.Authentication.States;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.AccessControl;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AllCustomers : IDisposable
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default;
        private HttpClient? _httpClient;
        [Inject]
        public AppState appState { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        private EditContext? editContext;


        private string searchQuery = string.Empty;
        private List<ApplicationUserDTO> filteredCustomers = new List<ApplicationUserDTO>();

        private List<ApplicationUserDTO> allCustomers = new List<ApplicationUserDTO>();
        private List<OrderDto> allOrders = new List<OrderDto>();
        private List<AdressDto> allAdress = new List<AdressDto>();
        //private List<ApplicationUserWithDetailsDTO> combinedCustomersWithDetails = new List<ApplicationUserWithDetailsDTO>();
        //private List<OrderItemDto> allOrderItems = new List<OrderItemDto>();
        //private List<ProductDto> allProducts = new List<ProductDto>();

        private int? expandedOrderId = null;
        private AdressDto updateAdress;
        private ApplicationUserDTO CustomerToEdit;
        private int? selectedOrderId;

        protected string message = string.Empty;
        protected string statusClass = string.Empty;

        protected bool isEditing = false;

        protected bool isProductSaved { get; set; }
        private bool _isDisposed = false;

        protected override async Task OnInitializedAsync()
        {

            if (Constants.JWTToken == "")
            {
                NavManager.NavigateTo("/login");
                return;
            }
            else
            {
                var token = DecryptJWTService.DecryptToken(Constants.JWTToken);
                if (token.Role == "Admin")
                {
                _httpClient = HttpClientFactory.CreateClient("Api");
                _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);
                await FetchLists();
                    StateHasChanged();
                }
            }
        }

        private async Task FetchLists()
        {
            try
            {
                 await FetchCustomers();
                if (allCustomers != null && allCustomers.Any())
                {
                    FetchAdresses();
                    FetchOrders();
                    //FetchProducts();
                    //try
                    //{
                    //    FetchOrderItems();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($"orderitems failed: {ex.Message}");
                    //}
                }


                //if (allCustomers != null && allCustomers.Any() && allAdress != null && allAdress.Any() && allOrders != null && allOrders.Any())
                //{
                //    combinedCustomersWithDetails = allCustomers.Select(customer => new ApplicationUserWithDetailsDTO
                //    {
                //        Customer = customer,
                //        Adress = allAdress.FirstOrDefault(adress => adress.AdressId == customer.AdressId),
                //        Orders = allOrders.Where(order => order.UserID == customer.UserId)
                //        .Select(order => new OrderWithDetailsDto
                //        {
                //            Order = order,
                //            OrderItems = allOrderItems.Where(item => item.OrderId == order.OrderId).ToList()


                //        }).ToList()
                //    }).ToList();
                //    filteredCustomers = combinedCustomersWithDetails;
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during initialization: {ex.Message}");
            }
            filteredCustomers = allCustomers;
        }


        //private async Task FetchProducts()
        //{
        //    var response = _productService.GetProductsAsync();
        //    if (response != null)
        //    {
        //        allProducts = await response;
        //    }
        //}
        //private async Task FetchOrderItems()
        //{
        //    var response =  _orderItemService.GetAllOrdersItemAsync();
        //    if (response != null)
        //    {
        //        allOrderItems = await response;
        //    }
        //}

        private async Task FetchCustomers()
        {
            try
            {
                var response = _customerService.GetAllUsersAsync();
                if (response != null)
                {
                    allCustomers = await response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR HERE: {ex.Message}");
                throw;
            }
        }
        private async Task FetchAdresses()
        {
            var response = _adressService.GetAllAdressAsync();
            if (response != null)
            {
                allAdress = await response;
            }
        }
        private async Task FetchOrders()
        {
            try
            {
                var response = _orderService.GetAllOrdersAsync();
                //var response = await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/order");
                if (response != null)
                {
                    allOrders = await response;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
                message = "There was an error fetching the orders. Please try again later.";
                statusClass = "alert-danger"; // Display an error message in the UI
            }
        }
        private void ToggleOrderDetails(int orderId)
        {
            if (expandedOrderId == orderId)
            {
                expandedOrderId = null; // If clicked again, collapse the details
            }
            else
            {
                expandedOrderId = orderId; // Expand the details for the clicked order
            }
        }

        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredCustomers = allCustomers;
            }
            else
            {
                filteredCustomers = allCustomers
                    .Where(p => p.Email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            StateHasChanged();
        }
        private Task OnEditClick(ApplicationUserDTO customer)
        {
            Console.WriteLine("Edit button clicked");

            CustomerToEdit = customer;  
            editContext = new EditContext(CustomerToEdit);
            isEditing = true;
            updateAdress = allAdress.Find(a => a.AdressId == customer.AdressId);
           // StateHasChanged();
            //            List<OrderDto> editOrders = allOrders.Where(o => o.UserID == customer.UserId).ToList();

            //    new ApplicationUserDTO
            //{
            //    UserId = customer.UserId,
            //    Firstname = customer.Firstname,
            //    Lastname = customer.Lastname,
            //    Email = customer.Email,
            //    PhoneNo = customer.PhoneNo,
            //    AdressId = customer.AdressId,
            //    Adress = updateAdress,
            //    Orders = editOrders

            //};
            //   selectedOrderId = editOrders.Count > 0 ? editOrders[0].OrderId : (int?)null;
            return Task.CompletedTask;
        }

        private void CancelEdit()
        {
            isEditing = false;
            CustomerToEdit = null;
        }

        private async Task UpdateCustomer()
        {
            var response = _customerService.UpdateUserAsync(CustomerToEdit.UserId, CustomerToEdit);
            StateHasChanged();
        }
        public void Dispose()
        {
            // Set disposed flag to true when the component is disposed
            _isDisposed = true;
        }
        private async Task DebugAuthentication()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            Console.WriteLine($"Is Authenticated: {user.Identity.IsAuthenticated}");
            Console.WriteLine($"User Role: {user.IsInRole("Admin")}");
        }

    }
}