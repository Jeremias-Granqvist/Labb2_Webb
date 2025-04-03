using Labb2_Infrastructure.DTOExstension;
using Labb2_Infrastructure.UoW;
using Labb2_Shared.Dtos;
using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly ICustomerRepository _customerRepository;


        public CustomerService(IRepository<ApplicationUser> repository, ICustomerRepository customerRepository) 
        {
            _repository = repository;
            _customerRepository = customerRepository;
        }
        public async Task<ApplicationUser> CreateUserAsync(ApplicationUserDTO customerDto)
        {
            var customer = AutoMapper<ApplicationUserDTO, ApplicationUser>.Map(customerDto);

            return await _repository.AddAsync(customer);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAllUsersAsync()
        {
            var list = await _repository.GetAllAsync();
            var changedList = AutoMapper<ApplicationUser, ApplicationUserDTO>.MapListIenum(list);
            return changedList;

        }

        public async Task<ApplicationUserDTO> GetUsersByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return null;

            return AutoMapper<ApplicationUser, ApplicationUserDTO>.Map(customer);
        }

        public async Task<ApplicationUser> GetUsersWithAdressAsync(int customerId)
        {
            return await _customerRepository.GetUsersWithAdressAsync(customerId);
        }

        public async Task<ApplicationUser> GetUsersWithOrdersAsync(int customerId)
        {
            return await _customerRepository.GetUsersWithOrdersAsync(customerId);
        }

        public async Task<bool> UpdateUserAsync(int id, ApplicationUserDTO customerDto)
        {
            var CustomerToUpdate = await _repository.GetByIdAsync(id);
            if (CustomerToUpdate == null)
            {
                return false;
            }
            AutoMapper<ApplicationUserDTO, ApplicationUser>.Map(customerDto, CustomerToUpdate);

            return await _repository.UpdateAsync(CustomerToUpdate);
        }
    }
}
