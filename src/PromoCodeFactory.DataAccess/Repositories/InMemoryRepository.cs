using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            var clonedData = Data.Select(item => (T)item.Clone()).ToList();
            return Task.FromResult(clonedData.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            var entity = Data.FirstOrDefault(x => x.Id == id);
            if (entity is null)
            {
                return Task.FromResult<T>(null);
            }
            return Task.FromResult((T)entity.Clone());
        }

        public Task<Guid> CreateAsync(T entity) 
        {
            entity.Id = Guid.NewGuid();
            Data.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var employee = Data.FirstOrDefault(e => e.Id == id);
            if (employee is not null)
            {
                Data.Remove(employee);
            }
            await Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            var existingEntity = Data.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity is not null) // прям заменяем
            {
                var index = Data.IndexOf(existingEntity);
                Data[index] = entity;
            }
            return Task.CompletedTask;
        }
    }
}