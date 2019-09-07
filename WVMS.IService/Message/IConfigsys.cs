using System;
using System.Collections.Generic;
using System.Text;
using WVMS.IRepository;
using WVMS.Model.Message;

namespace WVMS.IService.Message
{
    public interface IConfigsys : IBaseRepository<Configsys>
    {
        /// <summary>
        /// key是否存在
        /// </summary>
        /// <param name="keyName">key</param>
        /// <returns></returns>
        bool IsExists(string keyName);

        /// <summary>
        /// 获取数据列表从缓存
        /// </summary>
        /// <returns></returns>
        List<Configsys> GetListByCache();

        /// <summary>
        /// 根据key获取vlaue
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        string GetValue(string keyName);

        /// <summary>
        /// 根据type获取vlaue的列表
        /// </summary>
        /// <param name="type">类型值</param>
        /// <returns></returns>
        List<Configsys> GetValuesByType(int type);

        /// <summary>
        /// 根据key获取vlaue
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool GetBoolValue(string keyName);
    }
}
