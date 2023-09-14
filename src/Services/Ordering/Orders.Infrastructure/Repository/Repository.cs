using Microsoft.EntityFrameworkCore;
using Orders.Domain.Interfaces;
using Orders.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly OrderingContext Db;
        protected readonly DbSet<TEntity> DbSet;
        
        public Repository(OrderingContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> SelectListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<TEntity?> SelectOneAsync(Expression<Func<TEntity, bool>> expression)
        {
            // 消除CS8063警告 
            // (1) 方法允许返回 null 值，可以将返回类型更改为可空类型: 例如 TEntity?
            // (2) 方法不允许返回 null 值，做出相应更改: 例如 throw new InvalidOperationException("Entity not found");
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(expression);
            //return await DbSet.AsNoTracking().SingleOrDefaultAsync(expression) ?? throw new InvalidOperationException("Entity not found");
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}
