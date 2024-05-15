using MeetupWebAPI.DAL.EFCore;
using MeetupWebAPI.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MeetupDbContext Context { get; set; }

        public RepositoryBase(MeetupDbContext context)
        {
            this.Context = context;
        }

        public IQueryable<T> GetAllWithInclude(Expression<Func<T, object>> includeExpression)
        {
            return this.Context.Set<T>().Include(includeExpression).AsNoTracking();
        }

        public IQueryable<T> GetByConditionWithInclude(Expression<Func<T, object>> includeExpression, Expression<Func<T, bool>> whereExpression)
        {
            return this.Context.Set<T>().Include(includeExpression).Where(whereExpression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        public async Task<bool> HasEntity(Expression<Func<T, bool>> anyExpression)
        {
            return await this.Context.Set<T>().AnyAsync(anyExpression);
        }
    }
}
