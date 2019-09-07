using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WVMS.Model.ViewModel.Base;

namespace WVMS.Common.Controllers
{
    public class BaseController : Controller
    {
        private int _userid = 0;
        public int UserId
        {
            get
            {
                if (User == null) return _userid;
                if (User.Identity != null)
                {
                    var claim = (User.Identity as ClaimsIdentity).Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid);
                    if (claim != null)
                        return int.Parse(claim.Value);
                }
                return _userid;
            }
            set { _userid = value; }
        }
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        protected ApiResult Success(string msg)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.success = true;
            apiResult.code = (int)ApiEnum.Status;
            apiResult.msg = msg;
            return apiResult;
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        protected ApiResult Failed(string msg)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.success = false;
            apiResult.code = (int)ApiEnum.Error;
            apiResult.msg = msg;
            return apiResult;
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected ApiResult AjaxResult(bool b)
        {
            if (b)
            {
                return Success("操作成功！");
            }
            else
            {
                return Failed("操作失败！");
            }
        }

        /// <summary>
        /// 列表请求结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="count">记录数</param>
        /// <returns></returns>
        protected ApiResult ListResult(dynamic data, int count)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.success = true;
            apiResult.msg = "";
            apiResult.data = data;
            apiResult.count = count;
            return apiResult;
        }

        /// <summary>
        /// 数据请求结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected ApiResult DataResult(dynamic data)
        {
            ApiResult apiResult = new ApiResult();
            apiResult.success = true;
            apiResult.msg = "";
            apiResult.data = data;
            apiResult.code = (int)ApiEnum.Status;
            return apiResult;
        }

        /// <summary>
        /// 获取缩略图路径
        /// </summary>
        /// <param name="imageUrl">图片地址</param>
        /// <param name="thumbName">缩略图标识</param>
        /// <returns></returns>
        protected string GeThumbImage(string imageUrl, string thumbName)
        {
            if (string.IsNullOrWhiteSpace(imageUrl) || string.IsNullOrWhiteSpace(thumbName))
                return string.Empty;

            return string.Format(imageUrl, thumbName);
        }
    }
}
