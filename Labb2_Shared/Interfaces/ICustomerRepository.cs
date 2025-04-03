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
        Task<ApplicationUser> CreateUserAsync(ApplicationUser customer);

        Task<ApplicationUser> GetUsersWithAdressAsync(int customerId);
        Task<ApplicationUser> GetUsersWithOrdersAsync(int customerId);
        Task<ApplicationUser> GetUserFromEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser> UpdateUserAsync(int id, ApplicationUser user);
        Task<bool> DeleteUserAsync(int id);
    }
}
