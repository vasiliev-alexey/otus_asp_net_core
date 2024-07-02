using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mappers
{
    public class PromoCodeMapper
    {
        public static PromoCode MapGromModel(GivePromoCodeRequest request, Preference preference, IEnumerable<Customer> customers) {

            var promocode = new PromoCode();
            promocode.Id = Guid.NewGuid();
            
            promocode.PartnerName = request.PartnerName;
            promocode.Code = request.PromoCode;
            promocode.ServiceInfo = request.ServiceInfo;
           
            promocode.BeginDate = DateTime.Now;
            promocode.EndDate = DateTime.Now.AddDays(30);

            promocode.Preference = preference;
            promocode.PreferenceId = preference.Id;

            promocode.Customers = new List<PromoCodeCustomer>();

            foreach (var item in customers)
            {
                promocode.Customers.Add(new PromoCodeCustomer()
                {

                    CustomerId = item.Id,
                    Customer = item,
                    PromoCodeId = promocode.Id,
                    PromoCode = promocode
                });
            };

            return promocode;
        }
    }
}
