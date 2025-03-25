using Labb2_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.UoW;

//public class UnitOfWork
//{
//    private readonly StoreContext _context;
//    public CustomerRepository Customers { get; }
//    public ProductRepository Products { get; }
//    public UnitOfWork(StoreContext context)
//    {
//        _context = context;
//        Customers = new CustomerRepository(context);
//        Products = new ProductRepository(context);
//    }

//    public async Task SaveChangesAsync()
//    {
//        await _context.SaveChangesAsync();
//    }
//}
