using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region 查询
        Task<IEnumerable<TEntity>> SelectListAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity?> SelectOneAsync(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region 增加
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        #endregion

        #region 修改
        void Update(TEntity entity);
        #endregion

        #region 删除
        void Delete(TEntity entity);
        #endregion

        //// 实现事务提交
        //Task<bool> SaveAsync();
    }
}
