using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public class CustomerService(
    IRepository<Customer> customerRepository,
    IRepository<Preference> preferenceRepository,
    IRepository<PromoCode> promoCodeRepository) : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        return customer;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer) => await customerRepository.AddAsync(customer);

    public async Task UpdateCustomer(Customer customer) => await customerRepository.UpdateAsync(customer);

    public async Task DeleteCustomer(Customer customer) => await customerRepository.DeleteAsync(customer);


    public async Task<IEnumerable<Preference>> GetPreferencesAsync(List<Guid> ids)
    {
        IEnumerable<Preference> preferences = new List<Preference>();
        if (ids is { Count: > 0 })
        {
            Expression<Func<Preference, bool>> expression = x => ids.Any(item => item == x.Id);
            return await preferenceRepository.GetByConditionAsync(expression);
        }

        return await Task.FromResult(preferences);
    }

    public async Task GivePromocodesToCustomersWithPreferenceAsync(PromoCode promoCode, string preferenceName)
    {
        var prefs = await preferenceRepository.GetByConditionAsync(x => x.Name == preferenceName);

        var preferences = prefs as Preference[] ?? prefs.ToArray();
        if (preferences.Length == 0)
        {
            throw new Exception("Не найдено определенное предпочтение");
        }

        var pref = preferences.First();


        var customers =
            await customerRepository.GetByConditionAsync(x => x.Preferences.Any(y => y.Preference.Id == pref.Id));

        
        promoCode.BeginDate = DateTime.Now;
        promoCode.EndDate = DateTime.Now.AddDays(14);
        
        foreach (var customer in customers)
        {
            customer.PromoCodes.Add(promoCode);
            await customerRepository.UpdateAsync(customer);
        }
        
    }
}