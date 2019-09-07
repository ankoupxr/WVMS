using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WVMS.Model;

namespace WVMS.IRepository
{
    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        #region 同步

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        bool Add(T entity, bool autoSave = true);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        bool AddRange(IEnumerable<T> entities, bool autoSave = true);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        bool Update(T entity, bool autoSave = true);

        /// <summary>
        /// 更新部分属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <param name="updatedProperties">要更新的字段</param>
        /// <returns></returns>
        bool Update(T entity, bool autoSave = true, params Expression<Func<T, object>>[] updatedProperties);

        /// <summary>
        /// 更新实体部分属性,泛型方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <param name="updatedProperties">要更新的字段</param>
        /// <returns></returns>
        bool Update<Entity>(Entity entity, bool autoSave = true, params Expression<Func<Entity, object>>[] updatedProperties) where Entity : class;

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="autoSave">是否立即执行保存</param>
        bool UpdateRange(IEnumerable<T> entities, bool autoSave = true);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        bool Delete(T entity, bool autoSave = true);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="T">对象集合</param>
        /// <returns></returns>
        bool Delete(IEnumerable<T> entities);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="T">对象集合</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        bool Delete(IEnumerable<T> entities, bool autoSave = true);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetList();

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="top">前几条</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        IQueryable<T> GetList(int top, string predicate, string ordering, params object[] args);

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> predicat);

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        T GetModel(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">条件</param>
        /// <param name="ordering">排序</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        IQueryable<T> GetPagedList(int pageIndex, int pageSize, string predicate, string ordering, params object[] args);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="args">条件参数</param>
        /// <returns></returns>
        int GetRecordCount(string predicate, params object[] args);

        /// <summary>
        /// 保存
        /// </summary>
        int Save();

        #endregion
    }
}
