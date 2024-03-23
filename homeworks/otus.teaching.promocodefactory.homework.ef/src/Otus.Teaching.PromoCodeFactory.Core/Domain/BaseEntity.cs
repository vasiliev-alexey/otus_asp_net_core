using System;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [MaxLength(36)]
        public Guid Id { get; set; }
    }
}