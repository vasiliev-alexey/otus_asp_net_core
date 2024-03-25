using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public class PreferenceService  (IRepository <Preference> repository) : IPreferenceService
{
    public async Task<IEnumerable<Preference>> GetAllPreferencesAsync() => await repository.GetAllAsync();
   
}