using EshopApi.Models;
using EShopAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository= customerRepository;
        }
        // Get 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ObjectResult(await _customerRepository.GetCustomersAsync())
            {
                StatusCode = (int)HttpStatusCode.OK,
            };

            Request.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            Request.HttpContext.Response.Headers.Add("X-Name", "Abbas-Davodkhani");

            return result;
        }
        // Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id == 0) { return NotFound(); }
            if (!await IsExistsCustomer(id)) { return NotFound(); }

            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return Ok(customer);
        }
        // Post
        [HttpPost]
        public async Task<IActionResult> CraeteCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddAsync(customer);
                return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
            }
            return BadRequest(ModelState);
        }

        // Put
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (id == 0) { return NotFound(); }
            if (!await IsExistsCustomer(id)) { return NotFound(); }

            if (ModelState.IsValid)
            {
                await _customerRepository.UpdateAsync(customer);
                return Ok(customer);
            }

            return BadRequest(ModelState);

        }
        // Delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (id == 0) { return NotFound(); }
            if (!await IsExistsCustomer(id)) { return NotFound(); }

            await _customerRepository.DeleteAsync(id);
            return Ok();
        }

        private async Task<bool> IsExistsCustomer(int id)
        {
            return await _customerRepository.IsExistsCusotmerAsync(id);   
        }
    }
}
