using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public class EmployeeService(IRepository<Employee> repository) : IEmployeeService


{
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return repository.GetAllAsync();
    }

    public Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        return repository.GetByIdAsync(id);
    }

    public Task<Employee> AddEmployeeAsync(Employee employee)
    {
        return repository.AddAsync(employee); 
    }
}