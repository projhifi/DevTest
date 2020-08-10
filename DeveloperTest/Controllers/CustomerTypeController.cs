using System;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("ref-data")]
    public class ReferenceDataController : ControllerBase
    {
        private static readonly string[] CustomerTypes = { "Large", "Small" };

        [HttpGet]
        [Route("customer-types")]
        public IActionResult GetCustomerTypes()
        {
            return Ok(CustomerTypes);
        }
    }
}