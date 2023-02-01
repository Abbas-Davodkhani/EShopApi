using EshopApi.Models;

namespace EShopAPI.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByNameAsync(string name);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);  
        Task<Customer> DeleteAsync(int id); 
        Task<int> CustomerCountAsync();
        Task<bool> IsExistsCusotmerAsync(int id);
    }
}
