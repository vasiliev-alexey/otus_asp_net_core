﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения клиентов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController
        : ControllerBase
    {
        private readonly IRepository<Preference> _preferencesRepository;

        public PreferencesController(IRepository<Preference> preferencesRepository)
        {
            _preferencesRepository = preferencesRepository;
        }
        
        /// <summary>
        /// Получить список предпочтений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesAsync()
        {
            var preferences = await _preferencesRepository.GetAllAsync();

            var response = preferences.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(response);
        }
    }
}