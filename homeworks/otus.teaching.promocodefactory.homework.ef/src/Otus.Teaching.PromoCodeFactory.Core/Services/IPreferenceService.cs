using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public interface IPreferenceService
{
    Task<IEnumerable<Preference>> GetAllPreferencesAsync();
    
}