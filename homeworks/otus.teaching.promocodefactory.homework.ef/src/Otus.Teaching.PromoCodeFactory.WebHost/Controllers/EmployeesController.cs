using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Services;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        : ControllerBase
    {
        //   private readonly IRepository<Employee> _employeeRepository;

        // public EmployeesController(IRepository<Employee> employeeRepository)
        // {
        //     _employeeRepository = employeeRepository;
        // }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await employeeService.GetAllEmployeesAsync();

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
        /// Получить данные сотрудника по id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            ;

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Role = new RoleItemResponse()
                {
                    Name = employee.Role.Name,
                    Description = employee.Role.Description
                },
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }


        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> AddEmployeeAsync(EmployeeRequest employeeRequest)
        {
            logger.LogDebug("AddEmployeeAsync");
            var employee = new Employee()
            {
                Email = employeeRequest.Email,
                LastName = employeeRequest.LastName,
                FirstName = employeeRequest.FirstName,
                Role = employeeRequest.Role == null
                    ? null
                    : new Role() { Name = employeeRequest.Role.Name, Description = employeeRequest.Role.Description },
            };

            var addedEmployee = await employeeService.AddEmployeeAsync(employee);
            if (addedEmployee == null)
            {
                logger.LogError($"Ошибка при добавлении сотрудника {employeeRequest.Email}");
                return BadRequest();
            }

            var employeeResponse = new EmployeeResponse
            {
                Id = addedEmployee.Id,
                Email = addedEmployee.Email,
                Role = new RoleItemResponse
                    { Name = addedEmployee.Role.Name, Description = addedEmployee.Role.Description },
                FullName = addedEmployee.FullName
            };

            return Ok(employeeResponse);
        }
    }
}