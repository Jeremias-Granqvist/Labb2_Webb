using Labb2_Blazor.State;
using Labb2_Infrastructure.DTOExstension;
using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Labb2_Blazor.Components.Pages
{
    public partial class AllCustomers
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; } = default;
        private HttpClient? _httpClient;
        [Inject]
        public AppState appState { get; set; }

        private string searchQuery = string.Empty;
        private List<CustomerWithDetailsDto> filteredCustomers = new List<CustomerWithDetailsDto>();
        
        private List<CustomerDto> allCustomers = new List<CustomerDto>();
        private List<OrderDto> allOrders = new List<OrderDto>();
        private List<AdressDto> allAdress = new List<AdressDto>();
        private List<CustomerWithDetailsDto> combinedCustomersWithDetails = new List<CustomerWithDetailsDto>();
        
        private AdressDto updateAdress;
        private CustomerDto CustomerToEdit;
        private int? selectedOrderId;

        protected string message = string.Empty;
        protected string statusClass = string.Empty;

        protected bool isEditing = false;

        protected bool isProductSaved { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("Api");
            try
            {
                await FetchCustomers();
                if (allCustomers != null && allCustomers.Any())
                {
                    await FetchAdresses();
                    await FetchOrders();
                }

                if (allCustomers != null && allCustomers.Any() && allAdress != null && allAdress.Any() && allOrders != null && allOrders.Any())
                {
                    combinedCustomersWithDetails = allCustomers.Select(customer => new CustomerWithDetailsDto
                    {
                        Customer = customer,
                        Adress = allAdress.FirstOrDefault(adress => adress.AdressId == customer.AdressId),
                        Orders = allOrders.Where(order => order.CustomerId == customer.CustomerId).ToList()
                    }).ToList();
                    filteredCustomers = combinedCustomersWithDetails;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during initialization: {ex.Message}");
            }
        }
        
        //private async Task FetchOrderItems()
        //{
        //    var response = await _httpClient.GetFromJsonAsync<List<OrderItemDto>>("api/OrderItem")
        //}

        private async Task FetchCustomers()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customer");
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

        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredCustomers = combinedCustomersWithDetails;
            }
            else
            {
                filteredCustomers = combinedCustomersWithDetails
                    .Where(p => p.Customer.Firstname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || p.Customer.Lastname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            StateHasChanged();
        }
        private void OnEditClick(CustomerDto customer)
        {
            updateAdress = allAdress.Find(a => a.AdressId == customer.AdressId);
            List<OrderDto> editOrders = allOrders.Where(o => o.CustomerId == customer.CustomerId).ToList();

            CustomerToEdit = new CustomerDto
            {
                CustomerId = customer.CustomerId,
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
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{CustomerToEdit.CustomerId}", CustomerToEdit);
            Console.WriteLine($"id is: {CustomerToEdit.CustomerId}");

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