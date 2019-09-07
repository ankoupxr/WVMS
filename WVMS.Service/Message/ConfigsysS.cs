using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WVMS.Core.DB;
using WVMS.Infrastructure;
using WVMS.Infrastructure.Extentions;
using WVMS.IService.Message;
using WVMS.Model.Message;
using WVMS.Repository;

namespace WVMS.Service.Message
{
    public class ConfigsysS : BaseRepository<Configsys>,IConfigsys
    {
        ICacheService _cacheService;
        public ConfigsysS(MyDbContext dbcontext, ICacheService cacheService) : base(dbcontext)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        /// key是否存在
        /// </summary>
        /// <param name="keyName">key</param>
        /// <returns></returns>
        public bool IsExists(string keyName)
        {
            return base.IsExist(c => c.KeyName == keyName);
        }

        #region 获取数据列表从缓存
        /// <summary>
        /// 获取数据列表从缓存
        /// </summary>
        /// <returns></returns>
        public List<Configsys> GetListByCache()
        {
            List<Configsys> list = new List<Configsys>();
            if (_cacheService.Exists(CacheKey.CONFIGSYSTEMLISTALL))
            {
                list = _cacheService.GetCache<List<Configsys>>(CacheKey.CONFIGSYSTEMLISTALL);
            }
            else
            {
                list = GetList().AsNoTracking().ToList();//dbcontext不进行跟踪，去缓存
                _cacheService.SetCache(CacheKey.CONFIGSYSTEMLISTALL, list, 30);
            }
            return list;
        }
        #endregion

        #region 根据type获取vlaue的列表
        /// <summary>
        /// 根据type获取vlaue的列表
        /// </summary>
        /// <param name="type">类型值</param>
        /// <returns></returns>
        public List<Configsys> GetValuesByType(int type)
        {
            List<Configsys> list = GetListByCache();
            var l = list.Where(c => c.KeyType == type);
            return l.ToList();
        }
        #endregion

        #region 根据key获取vlaue
        /// <summary>
        /// 根据key获取vlaue
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetValue(string keyName)
        {
            List<Configsys> list = GetListByCache();
            var model = list.Where(c => c.KeyName == keyName).FirstOrDefault();
            if (model == null)
            {
                return "";
            }
            else
            {
                return model.KeyValue;
            }
        }
        #endregion

        #region 根据key获取vlaue
        /// <summary>
        /// 根据key获取vlaue
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool GetBoolValue(string keyName)
        {
            List<Configsys> list = GetListByCache();
            var model = list.Where(c => c.KeyName == keyName).FirstOrDefault();
            if (model == null)
            {
                return false;
            }
            else
            {
                return model.KeyValue.ToBool();
            }
        }
        #endregion
    }
}
