using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers;

/// <summary>
///     Клиенты
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController(
    IRepository<Customer> customerRepository,
    IRepository<Preference> preferenceRepository)
    : ControllerBase
{
    /// <summary>
    ///     Получить список клиентов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync()
    {
        var customers = await customerRepository.GetAllAsync();

        var response = customers.Select(x => new CustomerShortResponse
        {
            Id = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName
        }).ToList();

        return Ok(response);
    }

    /// <summary>
    ///     Получить клиента по id
    /// </summary>
    /// <param name="id">Id клиента, например
    ///     <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example>
    /// </param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        var response = new CustomerResponse(customer);

        return Ok(response);
    }

    /// <summary>
    ///     Создать нового клиента
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> CreateCustomerAsync(CreateOrEditCustomerRequest request)
    {
        //Получаем предпочтения из бд и сохраняем большой объект
        var preferences = await preferenceRepository
            .GetRangeByIdsAsync(request.PreferenceIds);

        var customer = CustomerMapper.MapFromModel(request, preferences);

        await customerRepository.AddAsync(customer);

        return CreatedAtAction("GetCustomerAsync", new { id = customer.Id }, customer.Id);
    }

    /// <summary>
    ///     Обновить клиента
    /// </summary>
    /// <param name="id">Id клиента, например
    ///     <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example>
    /// </param>
    /// <param name="request">Данные запроса></param>
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

    /// <summary>
    ///     Удалить клиента
    /// </summary>
    /// <param name="id">Id клиента, например
    ///     <example>a6c8c6b1-4349-45b0-ab31-244740aaf0f0</example>
    /// </param>
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