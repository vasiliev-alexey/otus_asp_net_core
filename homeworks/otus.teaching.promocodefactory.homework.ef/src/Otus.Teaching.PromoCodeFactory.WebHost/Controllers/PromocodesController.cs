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
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController(ICustomerService customerService, IPromoCodeService promoCodeService)
        : ControllerBase
    {
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promoCodes = await promoCodeService.GetAllPromocodesAsync();

            var resp = promoCodes.Select(x => new PromoCodeShortResponse
            {
                Code = x.Code, ServiceInfo = x.ServiceInfo, PartnerName = x.PartnerName, Id = x.Id,
                BeginDate = x.BeginDate.ToShortDateString(), EndDate = x.EndDate.ToShortDateString()
            }).ToList();
            return Ok(resp);
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            var promoCode = new PromoCode()
            {
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                PartnerName = request.PartnerName
            };
            await customerService.GivePromocodesToCustomersWithPreferenceAsync(promoCode, request.Preference);
            return Ok("Промокод успешно выдан клиентам с указанным предпочтением.");
        }
    }
}