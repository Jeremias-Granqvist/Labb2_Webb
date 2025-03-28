using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);

        Task<Customer> GetCustomerWithAdressAsync(int customerId);
        Task<Customer> GetCustomerWithOrdersAsync(int customerId);

        Task<IEnumerable<Customer>> GetCustomerAsync();

        Task<bool> DeleteCustomerAsync(int id);
    }
}
