using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PreferencesController(IPreferenceService preferenceService) : Controller
{
   
    /// <summary>
    /// Получить список предпочтений.
    /// </summary>
    /// <returns>Список предпочтений</returns>

    [HttpGet]
    public async Task<List<PreferenceResponse>> GetPreferencesAsync()
    {
        var preferences = await preferenceService.GetAllPreferencesAsync();
        var preferenceModelList = preferences.Select(x => new PreferenceResponse { Id = x.Id, Name = x.Name }).ToList();
        return preferenceModelList;
    }
}