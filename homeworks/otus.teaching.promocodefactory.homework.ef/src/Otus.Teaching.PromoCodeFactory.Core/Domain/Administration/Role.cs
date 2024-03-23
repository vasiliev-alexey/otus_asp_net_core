using System;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : BaseEntity
    {
        [Required] [MaxLength(50)] public string Name { get; set; }
        [Required] [MaxLength(250)] public string Description { get; set; }
    }
}