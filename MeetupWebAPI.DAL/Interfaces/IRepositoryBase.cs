using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Interfaces
{
    public interface IRepositoryBase<T> where T: class
    {
        IQueryable<T> GetAllWithInclude(Expression<Func<T, object>> includeExpression);
        IQueryable<T> GetByConditionWithInclude(Expression<Func<T, object>> includeExpression, Expression<Func<T, bool>> whereExpression);       

        Task<bool> HasEntity(Expression<Func<T, bool>> anyExpression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
