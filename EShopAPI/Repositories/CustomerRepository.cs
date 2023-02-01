using EshopApi.Models;
using EShopAPI.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EShopAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EshopApi_DBContext _context;
        private readonly IMemoryCache _memoryCache;
        public CustomerRepository(EshopApi_DBContext context , IMemoryCache memoryCache) 
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<int> CustomerCountAsync()
        {
            return await _context.Customer.CountAsync();
        }

        public async Task<Customer> DeleteAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);   
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var catchCustomer = _memoryCache.Get<Customer>(id);
            if (catchCustomer != null)
            {
                return catchCustomer;
            }
            else
            {
                var customer = await _context.Customer.SingleAsync(c => c.CustomerId == id);
                var catchOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _memoryCache.Set(customer.CustomerId, catchOption);
                return customer;
            }
        }

        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            return await _context.Customer.Include(x =>x.Orders).SingleAsync(x => x.FirstName == name);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<bool> IsExistsCusotmerAsync(int id)
        {
            return await _context.Customer.AnyAsync(c => c.CustomerId == id);
        }
    }
}
