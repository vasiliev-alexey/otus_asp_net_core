using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;

namespace Otus.Teaching.Pcf.Administration.WebHost.Services
{
    public class EmployeeService(IRepository<Employee> employeeRepository) : IEmployeeService
    {
        public async Task UpdateAppliedPromocodes(Guid id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("employee not found");

            employee.AppliedPromocodesCount++;

            await employeeRepository.UpdateAsync(employee);
        }
    }
}