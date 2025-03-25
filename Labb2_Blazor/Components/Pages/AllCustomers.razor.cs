using Labb2_Blazor.State;
using Labb2_Shared.Dtos;
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
        private List<CustomerDto> allCustomers = new List<CustomerDto>();
        private List<CustomerDto> filteredCustomers = new List<CustomerDto>();
        private List<OrderDto> allOrders = new List<OrderDto>();

        protected string message = string.Empty;
        protected string statusClass = string.Empty;

        private CustomerDto CustomerToEdit;
        protected bool isEditing = false;

        protected bool isProductSaved { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("Api");
            await FetchCustomers();
            await FetchOrders();
        }


        private async Task FetchCustomers()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customer");
            if (response != null)
            {
                allCustomers = response;
                filteredCustomers = response;
            }
        }

        private async Task FetchOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/order");
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
                    .Where(p => p.Firstname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || p.Lastname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            StateHasChanged();
        }
        private void OnEditClick(CustomerDto customer)
        {
            CustomerToEdit = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Email = customer.Email,
                PhoneNo = customer.PhoneNo,
                AdressId = customer.AdressId,
                Adress = customer.Adress,
                Orders = customer.Orders
            };
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
        }

        private async Task DeleteProduct()
        {
            var response = await _httpClient.DeleteFromJsonAsync<HttpResponseMessage>($"api/customer/{CustomerToEdit.CustomerId}");
            CheckResult(response);

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