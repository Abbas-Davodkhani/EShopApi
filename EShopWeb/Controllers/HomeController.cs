using EShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerRepository _customerRepository;

        public HomeController(ILogger<HomeController> logger , ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View(_customerRepository.GetCustomers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCustomer(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            _customerRepository.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}