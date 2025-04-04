using Labb2_Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        IAdressRepository Adress { get; }
        //IOrderitemRepository OrderItem { get; }
        
        Task<int> CompleteAsync();
    }
}
