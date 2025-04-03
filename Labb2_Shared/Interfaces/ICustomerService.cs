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
        Task<ApplicationUser> CreateUserAsync(ApplicationUserDTO customerDto);
        Task<List<ApplicationUserDTO>> GetAllUsersAsync();
        Task<ApplicationUserDTO> GetUsersByIdAsync(int id);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUsersWithAdressAsync(int customerId);
        Task<ApplicationUser> GetUsersWithOrdersAsync(int customerId);
        Task<bool> DeleteUserAsync(int id);

        Task<bool> UpdateUserAsync(int id, ApplicationUserDTO customerUpdateDto);
    }
}
