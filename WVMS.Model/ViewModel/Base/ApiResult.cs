
namespace WVMS.Model.ViewModel.Base
{
    /// <summary>
    /// 请求返回统一格式
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiResult()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="code">状态码</param>
        /// <param name="list">数据</param>
        /// <param name="recordCount">总记录数</param>
        public ApiResult(bool success, string msg, int code, dynamic list, int recordCount)
        {
            this.success = success;
            this.msg = msg;
            this.code = code;
            this.data = list;
            this.count = recordCount;
        }

        /// <summary>
        /// 构造函数，成功返回列表
        /// </summary>
        /// <param name="list">数据</param>
        /// <param name="recordCount">总记录数</param>
        public ApiResult(dynamic list, int recordCount)
        {
            this.success = true;
            this.data = list;
            this.count = recordCount;
        }

        /// <summary>
        /// 构造函数，操作是否成功
        /// </summary>
        /// <param name="list">数据</param>
        /// <param name="code">状态码</param>
        /// <param name="recordCount">总记录数</param>
        public ApiResult(bool success, int code, string msg)
        {
            this.success = success;
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        /// 构造函数，操作是否成功
        /// </summary>
        /// <param name="list">数据</param>
        /// <param name="recordCount">总记录数</param>
        public ApiResult(bool success, string msg)
        {
            this.success = success;
            if (success)
            {
                this.code = 200;
            }
            else
            {
                this.code = 500;
            }
            this.msg = msg;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { set; get; } = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int count { set; get; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic data { set; get; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 序列化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}