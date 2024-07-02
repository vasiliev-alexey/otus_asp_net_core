using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Repositories;

public class MongoDbRepository<T>(IMongoCollection<T> mongoCollection) : IRepository<T>
    where T : BaseEntity
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return (await mongoCollection.FindAsync(_ => true)).ToEnumerable();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return (await mongoCollection.FindAsync(entity => entity.Id == id)).FirstOrDefault();
    }

    public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
    {
        return (await mongoCollection.FindAsync(entity => ids.Contains(entity.Id))).ToEnumerable();
    }

    public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
    {
        return (await mongoCollection.FindAsync(predicate)).FirstOrDefault();
    }

    public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
    {
        return (await mongoCollection.FindAsync(predicate)).ToEnumerable();
    }

    public Task AddAsync(T entity)
    {
        return mongoCollection.InsertOneAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        return mongoCollection.ReplaceOneAsync(baseEntity => baseEntity.Id == entity.Id, entity);
    }

    public Task DeleteAsync(T entity)
    {
        return mongoCollection.DeleteOneAsync(baseEntity => baseEntity.Id == entity.Id);
    }
}