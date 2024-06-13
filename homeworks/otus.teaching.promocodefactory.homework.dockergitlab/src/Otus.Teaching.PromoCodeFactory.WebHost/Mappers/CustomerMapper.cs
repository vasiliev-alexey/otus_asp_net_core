using System.Collections.Generic;
using System.Linq;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mappers
{
    public static class CustomerMapper
    {
        public static Customer MapFromModel(CreateOrEditCustomerRequest request, 
            IEnumerable<Preference> preferences, Customer customer = null)
        {
            if (customer == null)
            {
                customer = new Customer();
            }
            
            customer.Email = request.Email;
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            
            if (preferences != null && preferences.Any())
            {
                customer.Preferences?.Clear();
                customer.Preferences = preferences.Select(x => new CustomerPreference()
                {
                    Customer = customer,
                    Preference = x
                }).ToList();
            }

            return customer;
        }
    }
}