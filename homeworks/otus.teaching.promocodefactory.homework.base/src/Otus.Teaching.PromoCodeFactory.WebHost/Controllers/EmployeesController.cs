using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController(IRepository<Employee> employeeRepository, ILogger<EmployeesController> logger)
        : ControllerBase
    {
        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        ///  <summary>
        ///  Добавить нового сотрудника
        ///  </summary>
        ///  <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> AddEmployeeAsync(EmployeeRequest employeeRequest)
        {
            logger.LogDebug("AddEmployeeAsync");
            var employee = new Employee()
            {
                Email = employeeRequest.Email,
                LastName = employeeRequest.LastName,
                FirstName = employeeRequest.FirstName,
                Roles = employeeRequest.Roles.Select(x => new Role()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList()
            };

            var addedEmployee = await employeeRepository.AddAsync(employee);
            if (addedEmployee == null)
            {
                logger.LogError($"Ошибка при добавлении сотрудника {employeeRequest.Email}");
                return BadRequest();
            }

            var employeeResponse = new EmployeeResponse
            {
                Id = addedEmployee.Id,
                Email = addedEmployee.Email,
                Roles = addedEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = addedEmployee.FullName
            };

            return Ok(employeeResponse);
        }


        /// <summary>
        /// Обновить данные сотрудника по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeAsync(Guid id,
            [FromBody] EmployeeRequest employeeRequest)
        {
            logger.LogDebug("UpdateEmployeeAsync with id: {id}", id);
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null) return NotFound();
            employee.Email = employeeRequest.Email;
            employee.LastName = employeeRequest.LastName;
            employee.FirstName = employeeRequest.FirstName;
            employee.Roles = employeeRequest.Roles
                .Select(x => new Role() { Name = x.Name, Description = x.Description }).ToList();
            var updatedEmployee = await employeeRepository.UpdateAsync(employee);

            if (updatedEmployee == null)
            {
                logger.LogError("Ошибка при обновлении сотрудника, id: {id}", id);
                return BadRequest();
            }

            var employeeResponse = new EmployeeResponse
            {
                Id = updatedEmployee.Id,
                Email = updatedEmployee.Email,
                Roles = updatedEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = updatedEmployee.FullName
            };

            return Ok(employeeResponse);
        }


        /// <summary> Удаление сотрудника по Id </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            await employeeRepository.DeleteAsync(employee);
            return Ok();
        }
    }
}