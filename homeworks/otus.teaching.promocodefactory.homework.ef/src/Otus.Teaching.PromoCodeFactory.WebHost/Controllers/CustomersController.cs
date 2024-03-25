using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    /// <example>
    /// GET: /api/v1/customers
    /// </example>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(ICustomerService customerService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            var customers = await customerService.GetAllCustomersAsync();
            var response = customers.Select(x => new CustomerShortResponse()
            {
                Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email,
            }).ToList();
            return Ok(response);
        }


        ///<summary>
        /// Get a customer with the specified Id
        ///</summary>
        ///<example>
        /// GET: /api/v1/customers/{id}
        ///</example>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer = await customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer not found with id: {id}");
            }

            var response = new CustomerResponse()
            {
                Id = customer.Id,
                FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email,
                PromoCodes = customer.PromoCodes.Select(x => new PromoCodeShortResponse()
                    {
                        Id = x.Id, Code = x.Code, BeginDate = x.BeginDate.ToLongDateString(),
                        EndDate = x.EndDate.ToLongDateString()
                    })
                    .ToList(),
                Preference = customer.Preferences.Select(x => new PreferenceResponse()
                {
                    Id = x.PreferenceId,
                    Name = x.Preference.Name
                }).ToList()
            };

            return Ok(response);
        }


        ///<summary>
        /// Create a new customer
        ///</summary>
        ///<example>
        /// POST: /api/v1/customers
        /// { ///     "firstName": "John",
        ///     "lastName": "Doe",
        ///     "email": "<EMAIL>"
        /// }
        ///</example>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var newCustomer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Preferences = request.PreferenceIds?.Select(x => new CustomerPreference()
                {
                    PreferenceId = x
                }).ToList(),
            };

            var created = await customerService.AddCustomerAsync(newCustomer);

            return CreatedAtAction(
                nameof(GetCustomerAsync),
                new { id = created.Id },
                null);
        }

        /// <summary>
        /// Edits a customer's information in the database.
        /// </summary>
        /// <param name="id">The ID of the customer to edit.</param>
        /// <param name="request">The updated customer information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound($"Customer not found with id: {id}");
            }

            var preferences = await customerService.GetPreferencesAsync(request.PreferenceIds);

            if (id != customer.Id)
            {
                return BadRequest("This customer cannot be modified");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.Preferences = preferences.Where(x => customer.Preferences.All(y => y.PreferenceId != x.Id))
                .Select(x => new CustomerPreference() { PreferenceId = x.Id }).ToList();
            await customerService.UpdateCustomer(customer);
            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an 
        /// <see cref="IActionResult"/> with a NoContent result if the customer was deleted successfully, 
        /// or a NotFound result if the customer was not found.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound($"Customer not found with id: {id}");

            await customerService.DeleteCustomer(customer);
            return NoContent();
        }
    }
}