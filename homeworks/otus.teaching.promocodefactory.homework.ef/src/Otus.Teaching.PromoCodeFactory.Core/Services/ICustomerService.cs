using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<Customer> AddCustomerAsync(Customer customer);
 
    
    Task UpdateCustomer(Customer customer);
    Task DeleteCustomer(Customer customer);


    Task<IEnumerable<Preference>> GetPreferencesAsync(List<Guid> requestPreferenceIds);
    Task GivePromocodesToCustomersWithPreferenceAsync(PromoCode promoCode, string preferenceName);
}