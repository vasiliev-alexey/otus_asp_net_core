using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Employee
        : BaseEntity
    {
        [Required ] 
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required ] 
        [MaxLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        [Required ] 
        [MaxLength(50)]
        public string Email { get; set; }

        public virtual Role Role { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}