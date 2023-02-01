using Newtonsoft.Json;
using System.Text;

namespace EShopWeb.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private string apiUrl = "http://localhost:5162/api/Customers";
        private HttpClient _client;

        public CustomerRepository()
        {
            _client = new HttpClient(); 
        }
        // Get
        public List<Customer> GetCustomers() 
        { 
            var result = _client.GetStringAsync(apiUrl).Result;

            var customers = JsonConvert.DeserializeObject<List<Customer>>(result);  

            return customers;
        }

        // Get By Id
        public Customer GetCustomer(int id)
        {
            var result = _client.GetStringAsync(apiUrl+"/"+id).Result;

            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        // Insert
        public void AddCustomer(Customer customer) 
        {
            var jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer , Encoding.UTF8 , "application/json");

            _client.PostAsync(apiUrl, content);
        }

        public void UpdateCustomer(Customer customer)
        {
            string jsonCustomer = JsonConvert.SerializeObject(customer);

            StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

            var res = _client.PutAsync(apiUrl+"/"+customer.CustomerId, content).Result;
        }

        public void DeleteCustomer(int customerId)
        {
            var res = _client.DeleteAsync(apiUrl + "/" + customerId).Result;
        }

    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
