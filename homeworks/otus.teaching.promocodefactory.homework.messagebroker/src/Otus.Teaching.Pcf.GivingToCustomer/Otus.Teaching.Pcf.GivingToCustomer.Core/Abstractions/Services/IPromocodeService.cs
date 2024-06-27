using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services
{
    public interface IPromocodeService<T>
    {
        Task GivePromoCodesToCustomersWithPreferenceAsync(T request);
    }
}
