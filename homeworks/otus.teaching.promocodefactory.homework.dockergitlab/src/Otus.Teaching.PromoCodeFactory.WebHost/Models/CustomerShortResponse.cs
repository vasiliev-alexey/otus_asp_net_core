using System;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class CustomerShortResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public CustomerShortResponse()
        {
            
        }

        public CustomerShortResponse(Customer customer)
        {
            Id = customer.Id;
            Email = customer.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
    }
}