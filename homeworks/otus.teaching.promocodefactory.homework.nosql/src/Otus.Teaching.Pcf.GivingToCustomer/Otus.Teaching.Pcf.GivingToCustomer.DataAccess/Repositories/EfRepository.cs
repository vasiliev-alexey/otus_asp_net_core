using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;

public class EfRepository<T>(DataContext dataContext) : IRepository<T>
    where T : BaseEntity
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await dataContext.Set<T>().ToListAsync();

        return entities;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
    {
        var entities = await dataContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        return entities;
    }

    public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
    {
        return await dataContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
    {
        return await dataContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await dataContext.Set<T>().AddAsync(entity);
        await dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dataContext.Set<T>().Remove(entity);
        await dataContext.SaveChangesAsync();
    }
}