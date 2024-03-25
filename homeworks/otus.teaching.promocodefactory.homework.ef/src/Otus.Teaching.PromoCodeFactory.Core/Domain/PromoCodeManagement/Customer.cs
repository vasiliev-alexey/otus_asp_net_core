using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    [Comment("Customer domain model.")]
    public class Customer
        :BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        [MaxLength(50)]
        public string Email { get; set; }

        public virtual ICollection<CustomerPreference> Preferences { get; set; } = new List<CustomerPreference>();

        public virtual ICollection<PromoCode> PromoCodes { get; set; }  = new List<PromoCode>();
        
    }
}