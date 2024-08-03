using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Mappers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(
        IRepository<Customer> customerRepository,
        IRepository<Preference> preferenceRepository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync()
        {
            var customers =  await customerRepository.GetAllAsync();

            var response = customers.Select(x => new CustomerShortResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();

            return Ok(response);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer =  await customerRepository.GetByIdAsync(id);

            var response = new CustomerResponse(customer);

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            //Получаем предпочтения из бд и сохраняем большой объект
            var preferences = await preferenceRepository
                .GetRangeByIdsAsync(request.PreferenceIds);

            Customer customer = CustomerMapper.MapFromModel(request, preferences);
            
            await customerRepository.AddAsync(customer);

            return CreatedAtAction(nameof(GetCustomerAsync), new {id = customer.Id}, customer.Id);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            
            if (customer == null)
                return NotFound();
            
            var preferences = await preferenceRepository.GetRangeByIdsAsync(request.PreferenceIds);
            
            CustomerMapper.MapFromModel(request, preferences, customer);

            await customerRepository.UpdateAsync(customer);

            return NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            
            if (customer == null)
                return NotFound();

            await customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}