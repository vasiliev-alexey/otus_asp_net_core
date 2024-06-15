﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.WebHost.Models;

namespace Otus.Teaching.Pcf.Administration.WebHost.Controllers;

/// <summary>
///     Роли сотрудников
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class RolesController(IRepository<Role> rolesRepository)
{
    /// <summary>
    ///     Получить все доступные роли сотрудников
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<RoleItemResponse>> GetRolesAsync()
    {
        var roles = await rolesRepository.GetAllAsync();

        var rolesModelList = roles.Select(x =>
            new RoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

        return rolesModelList;
    }
}