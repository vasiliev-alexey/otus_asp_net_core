using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.Core.Services;

public class PromoCodeService(IRepository<PromoCode> promoCodeRepository) : IPromoCodeService
{
    /// <summary>
    /// Получение всех купонов
    /// </summary>
    /// <returns>Список купонов</returns>
    public async Task<IEnumerable<PromoCode>> GetAllPromocodesAsync() => await promoCodeRepository.GetAllAsync();
}