using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKProject.Persistence.Context;
using IK.Domain.Entities.Abstract;
using IKProject.Application.Interfaces.Repositories;

namespace IKProject.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly private IKProjectDbContext _context;

        public WriteRepository(IKProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            model.Added = DateTime.Now;
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FindAsync(Guid.Parse(id));
            return Remove(model);

        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();


        public bool Update(T model)
        {
            model.Updated = DateTime.Now;
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
        public virtual Task<bool> RemoveRelatedRecordsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
