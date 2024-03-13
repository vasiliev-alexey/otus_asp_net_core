using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>(IEnumerable<T> items) : IRepository<T>
        where T : BaseEntity
    {
        private IEnumerable<T> Items { get; set; } = items;

        /// <summary>
        /// Asynchronously retrieves all items.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of items.</returns>
        public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(Items);


        /// <summary>
        /// Gets the item by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the item</param>
        /// <returns>The task representing the operation, with the result</returns>
        public Task<T> GetByIdAsync(Guid id) => Task.FromResult(Items.FirstOrDefault(x => x.Id == id));


        /// <summary>
        /// Asynchronously adds a new item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added item.</returns>
        public Task<T> AddAsync(T item)
        {
            item.Id = Guid.NewGuid();
            Items = Items.Append(item);
            return Task.FromResult(item);
        }

        public Task<T> UpdateAsync(T item)
        {
            var storedItem = Items.FirstOrDefault(x => x.Id == item.Id);
            if (storedItem != null)
            {
                storedItem = item;
            }
            return Task.FromResult(storedItem);
        }

        public Task DeleteAsync(T entity)
        {
            Items = Items.Where(x => x.Id != entity.Id);
            return Task.CompletedTask;
        }
    }
}