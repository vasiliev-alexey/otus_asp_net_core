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
///     Промокоды
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class PromocodesController(
    IRepository<PromoCode> promoCodesRepository,
    IRepository<Preference> preferencesRepository,
    IRepository<Customer> customersRepository)
    : ControllerBase
{
    /// <summary>
    ///     Получить все промокоды
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
    {
        var promocodes = await promoCodesRepository.GetAllAsync();

        var response = promocodes.Select(x => new PromoCodeShortResponse
        {
            Id = x.Id,
            Code = x.Code,
            BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
            EndDate = x.EndDate.ToString("yyyy-MM-dd"),
            PartnerId = x.PartnerId,
            ServiceInfo = x.ServiceInfo
        }).ToList();

        return Ok(response);
    }

    /// <summary>
    ///     Создать промокод и выдать его клиентам с указанным предпочтением
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
    {
        //Получаем предпочтение по имени
        var preference = await preferencesRepository.GetByIdAsync(request.PreferenceId);

        if (preference == null) return BadRequest();

        //  Получаем клиентов с этим предпочтением:
        var customers = await customersRepository
            .GetWhere(d => d.Preferences.Any(x =>
                x.Preference.Id == preference.Id));

        var promoCode = PromoCodeMapper.MapFromModel(request, preference, customers);

        await promoCodesRepository.AddAsync(promoCode);

        return CreatedAtAction("GetPromocodesAsync", new { }, null);
    }
}