using Labb2_Blazor.State;
using Labb2_Infrastructure.Authentication.States;
using Labb2_Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AllCustomers
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
        private List<ApplicationUserWithDetailsDTO> filteredCustomers = new List<ApplicationUserWithDetailsDTO>();

        private List<ApplicationUserDTO> allCustomers = new List<ApplicationUserDTO>();
        private List<OrderDto> allOrders = new List<OrderDto>();
        private List<AdressDto> allAdress = new List<AdressDto>();
        private List<ApplicationUserWithDetailsDTO> combinedCustomersWithDetails = new List<ApplicationUserWithDetailsDTO>();
        private List<OrderItemDto> allOrderItems = new List<OrderItemDto>();
        private List<ProductDto> allProducts = new List<ProductDto>();

        private int? expandedOrderId = null;
        private AdressDto updateAdress;
        private ApplicationUserDTO CustomerToEdit;
        private int? selectedOrderId;

        protected string message = string.Empty;
        protected string statusClass = string.Empty;

        protected bool isEditing = false;

        protected bool isProductSaved { get; set; }

        protected override async Task OnInitializedAsync()
        {

            if (Constants.JWTToken == "")
            {
                NavManager.NavigateTo("/login");
                return;
            }
            else
            {
                
                _httpClient = HttpClientFactory.CreateClient("Api");
                _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);
                await FetchLists();
            }
        }

        private async Task FetchLists()
        {
            try
            {
                await FetchCustomers();
                if (allCustomers != null && allCustomers.Any())
                {
                    await FetchAdresses();
                    await FetchOrders();
                    await FetchProducts();
                    try
                    {
                        await FetchOrderItems();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"orderitems failed: {ex.Message}");
                    }
                }


                if (allCustomers != null && allCustomers.Any() && allAdress != null && allAdress.Any() && allOrders != null && allOrders.Any())
                {
                    combinedCustomersWithDetails = allCustomers.Select(customer => new ApplicationUserWithDetailsDTO
                    {
                        Customer = customer,
                        Adress = allAdress.FirstOrDefault(adress => adress.AdressId == customer.AdressId),
                        Orders = allOrders.Where(order => order.UserID == customer.UserId)
                        .Select(order => new OrderWithDetailsDto
                        {
                            Order = order,
                            OrderItems = allOrderItems.Where(item => item.OrderId == order.OrderId).ToList()


                        }).ToList()
                    }).ToList();
                    filteredCustomers = combinedCustomersWithDetails;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during initialization: {ex.Message}");
            }

        }


        private async Task FetchProducts()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product");
            if (response != null)
            {
                allProducts = response;
            }
        }
        private async Task FetchOrderItems()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrderItemDto>>("api/OrderItems");
            if (response != null)
            {
                allOrderItems = response;
            }
        }

        private async Task FetchCustomers()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ApplicationUserDTO>>("api/customer");
            if (response != null)
            {
                allCustomers = response;
            }
        }
        private async Task FetchAdresses()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AdressDto>>("api/adress");
            foreach (var adress in response)
            {
                allAdress.Add(adress);
            }
        }
        private async Task FetchOrders()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/order");
                if (response != null)
                {
                    allOrders = response;
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
                filteredCustomers = combinedCustomersWithDetails;
            }
            else
            {
                filteredCustomers = combinedCustomersWithDetails
                    .Where(p => p.Customer.Email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            StateHasChanged();
        }
        private void OnEditClick(ApplicationUserDTO customer)
        {
            updateAdress = allAdress.Find(a => a.AdressId == customer.AdressId);
            List<OrderDto> editOrders = allOrders.Where(o => o.UserID == customer.UserId).ToList();

            CustomerToEdit = new ApplicationUserDTO
            {
                UserId = customer.UserId,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Email = customer.Email,
                PhoneNo = customer.PhoneNo,
                AdressId = customer.AdressId,
                Adress = updateAdress,
                Orders = editOrders

            };
            selectedOrderId = editOrders.Count > 0 ? editOrders[0].OrderId : (int?)null;

            isEditing = true;
        }

        private void CancelEdit()
        {
            isEditing = false;
            CustomerToEdit = null;
        }

        private async Task UpdateCustomer()
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{CustomerToEdit.UserId}", CustomerToEdit);
            Console.WriteLine($"id is: {CustomerToEdit.UserId}");

            CheckResult(response);
            StateHasChanged();
        }


        private async void CheckResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                await FetchCustomers();
                isEditing = false;
                CustomerToEdit = null;
            }
            else
            {
                Console.WriteLine("Failed to update product");
            }
            StateHasChanged();

        }
    }
}