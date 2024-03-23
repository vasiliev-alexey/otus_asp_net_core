using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models;

public class EmployeeRequest
{
    // public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public RoleItemRequest Role { get; set; }

}