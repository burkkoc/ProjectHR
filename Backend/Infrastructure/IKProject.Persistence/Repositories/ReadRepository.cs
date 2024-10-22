using IK.Domain.Entities.Abstract;
using IKProject.Application.Interfaces.Repositories;
using IKProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly IKProjectDbContext _context;

        public ReadRepository(IKProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public async Task<T> GetByIdAsync(string id) => await Table.FindAsync(Guid.Parse(id));


        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> exp) => await Table.FirstOrDefaultAsync(exp);


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> exp) => Table.Where(exp);
    }
}
