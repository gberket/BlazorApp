using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppModels;
using BlazorWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customerRepository;

        private const int DEFAULT_PAGESIZE = 2;
        private const int DEFAULT_PAGEINDEX = 1;

        public CustomersController(ICustomersRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetCustomers([FromQuery]int pageNumber = DEFAULT_PAGEINDEX, int pageSize = DEFAULT_PAGESIZE)
        {
            try
            {
                return Ok(await customerRepository.GetCustomersPaginated(pageNumber, pageSize));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        /// <summary>
        /// Find a Customer by his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            try
            {
                var result = await customerRepository.GetCustomer(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        /// <summary>
        /// Add a new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var createdCustomer = await customerRepository.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }

        }

        /// <summary>
        /// Update a specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string id, Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest();
                }
                var customerToUpdate = await customerRepository.GetCustomer(id);
                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }
                return await customerRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        /// <summary>
        /// Delete a specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            try
            {
                await customerRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}