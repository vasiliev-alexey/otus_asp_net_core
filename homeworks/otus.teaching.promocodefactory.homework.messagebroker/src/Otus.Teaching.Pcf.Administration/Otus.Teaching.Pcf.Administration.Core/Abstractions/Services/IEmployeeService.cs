using System;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.Core.Abstractions.Services
{
    public interface IEmployeeService
    {
        Task UpdateAppliedPromocodes(Guid id);
    }
}
