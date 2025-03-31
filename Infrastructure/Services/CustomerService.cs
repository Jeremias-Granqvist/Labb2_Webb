using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.UoW;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;


        public CustomerService(IRepository<Customer> repository) 
        {
            _repository = repository;
        }
        public async Task<Customer> CreateCustomerAsync(CustomerDto customerDto)
        {
            var customer = AutoMapper<CustomerDto, Customer>.Map(customerDto);

            return await _repository.AddAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync()
        {
            var list = await _repository.GetAllAsync();
            var changedList = AutoMapper<Customer, CustomerDto>.MapListIenum(list);
            return changedList;

        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return null;

            return AutoMapper<Customer, CustomerDto>.Map(customer);
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerDto customerDto)
        {
            var CustomerToUpdate = await _repository.GetByIdAsync(id);
            if (CustomerToUpdate == null)
            {
                return false;
            }
            AutoMapper<CustomerDto, Customer>.Map(customerDto, CustomerToUpdate);

            return await _repository.UpdateAsync(CustomerToUpdate);
        }
    }
}
