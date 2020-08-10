using Microsoft.AspNetCore.Mvc;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Models;
using System.Linq;
using System;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private static readonly string[] CustomerTypes = { "Large", "Small" };

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(customerService.GetCustomers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(BaseCustomerModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerName) || model.CustomerName.Length < 5)
            {
                return BadRequest("Name is required and must be minimum 5 characters long");
            }

            if (string.IsNullOrEmpty(model.CustomerType) ||
                !CustomerTypes.Contains(model.CustomerType, StringComparer.CurrentCultureIgnoreCase))
            {
                return BadRequest("Type is required and must be either 'Large' or 'Small'");
            }

            var customer = customerService.CreateCustomer(model);

            return Created($"customer/{customer.CustomerId}", customer);
        }
    }
}