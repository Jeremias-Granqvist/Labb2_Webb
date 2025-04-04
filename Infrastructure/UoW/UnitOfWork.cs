using Labb2_Infrastructure.Repositories;
using Labb2_Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _context;

    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public ICustomerRepository Customers { get; }
    public IAdressRepository Adress { get; }
    //public IOrderitemRepository OrderItem { get; }

    public UnitOfWork()
    {

        _context = new StoreContext();
        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
        Customers = new CustomerRepository(_context);
        Adress = new AdressRepository(_context);
        //OrderItem = new OrderItemRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
