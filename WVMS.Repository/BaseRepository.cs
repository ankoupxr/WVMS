using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using WVMS.IRepository;
using WVMS.Core.DB;

namespace WVMS.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class,new()
    {

        protected readonly MyDbContext _dbContext;
        public BaseRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 同步

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Any(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public bool Add(T entity, bool autoSave = true)
        {
            int row = 0;
            _dbContext.Set<T>().Add(entity);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public bool AddRange(IEnumerable<T> entities, bool autoSave = true)
        {
            int row = 0;
            _dbContext.Set<T>().AddRange(entities);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public bool Update(T entity, bool autoSave = true)
        {
            int row = 0;
            _dbContext.Update(entity);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 更新实体部分属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <param name="updatedProperties">要更新的字段</param>
        /// <returns></returns>
        public bool Update(T entity, bool autoSave = true, params Expression<Func<T, object>>[] updatedProperties)
        {
            int row = 0;
            //告诉EF Core开始跟踪实体的更改，
            //因为调用DbContext.Attach方法后，EF Core会将实体的State值
            //更改回EntityState.Unchanged，
            _dbContext.Attach(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    //告诉EF Core实体的属性已经更改。将属性的IsModified设置为true后，
                    //也会将实体的State值更改为EntityState.Modified，
                    //这样就保证了下面SaveChanges的时候会将实体的属性值Update到数据库中。
                    _dbContext.Entry(entity).Property(property).IsModified = true;
                }
            }

            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 更新实体部分属性,泛型方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <param name="updatedProperties">要更新的字段</param>
        /// <returns></returns>
        public bool Update<Entity>(Entity entity, bool autoSave = true, params Expression<Func<Entity, object>>[] updatedProperties) where Entity : class
        {
            int row = 0;
            //告诉EF Core开始跟踪实体的更改，
            //因为调用DbContext.Attach方法后，EF Core会将实体的State值
            //更改回EntityState.Unchanged，
            _dbContext.Attach(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    //告诉EF Core实体的属性已经更改。将属性的IsModified设置为true后，
                    //也会将实体的State值更改为EntityState.Modified，
                    //这样就保证了下面SaveChanges的时候会将实体的属性值Update到数据库中。
                    _dbContext.Entry(entity).Property(property).IsModified = true;
                }
            }

            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public bool UpdateRange(IEnumerable<T> entities, bool autoSave = true)
        {
            int row = 0;
            _dbContext.UpdateRange(entities);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public T GetModel(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public bool Delete(T entity, bool autoSave = true)
        {
            int row = 0;
            _dbContext.Set<T>().Remove(entity);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="T">对象集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            int row = _dbContext.SaveChanges();
            return (row > 0);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="T">对象集合</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> entities, bool autoSave = true)
        {
            int row = 0;
            _dbContext.Set<T>().RemoveRange(entities);
            if (autoSave)
                row = Save();
            return (row > 0);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetList()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="top">前几条</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(int top, string predicate, string ordering, params object[] args)
        {
            var result = _dbContext.Set<T>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(predicate))
                result = result.Where(predicate, args);

            if (!string.IsNullOrWhiteSpace(ordering))
                result = result.OrderBy(ordering);

            if (top > 0)
            {
                result = result.Take(top);
            }
            return result;
        }

        /// <summary>
        /// 分页查询,返回实体对象
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetPagedList(int pageIndex, int pageSize, 
            string predicate, string ordering, params object[] args)
        {
            var result = (from p in _dbContext.Set<T>()
                          select p).AsQueryable();

            if (!string.IsNullOrWhiteSpace(predicate))
                result = result.Where(predicate, args);

            if (!string.IsNullOrWhiteSpace(ordering))
                result = result.OrderBy(ordering);

            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        public virtual int GetRecordCount(string predicate, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(predicate))
            {
                return _dbContext.Set<T>().Count();
            }
            else
            {
                return _dbContext.Set<T>().Where(predicate, args).Count();
            }
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public int Save()
        {
            int result = _dbContext.SaveChanges();
            return result;
        }

        #endregion

    }
}
