using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models;

/// <summary>
/// Represents a response object for preference data.
/// </summary>
public class PreferenceResponse
{
    /// <summary>
    /// Id предпочитаемого продукта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя предпочитаемого продукта
    /// </summary>
    public string Name { get; set; }
}