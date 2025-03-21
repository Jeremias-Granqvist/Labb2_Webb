using Labb2_Shared.Dtos;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Shared.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(CustomerDto customerDto);
        Task<IEnumerable<CustomerDto>> GetAllCustomerAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<bool> DeleteCustomerAsync(int id);

        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateDto);
    }
}
