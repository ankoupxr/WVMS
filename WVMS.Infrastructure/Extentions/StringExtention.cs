using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace WVMS.Infrastructure.Extentions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        #region 类型转换

        /// <summary>
        /// 字符串转数字，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this string val)
        {
            if (IsBlank(val))
                return 0;
            int k;
            return int.TryParse(val, out k) ? k : 0;
        }

        /// <summary>
        /// 字符串转时间，未转换成功返回当前日期
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string val)
        {
            if (IsBlank(val))
                return DateTime.Now;
            DateTime k;
            return DateTime.TryParse(val, out k) ? k : DateTime.Now;
        }

        /// <summary>
        /// 字符串转时间，未转换成功返回00:00:00
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string val)
        {
            if (IsBlank(val))
                return TimeSpan.Zero;
            TimeSpan k;
            return TimeSpan.TryParse(val, out k) ? k : TimeSpan.Zero;
        }

        /// <summary>
        /// 字符串转bool类型，未转换成功返回false
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ToBool(this string val)
        {
            if (IsBlank(val))
                return false;
            bool k;
            return bool.TryParse(val, out k) ? k : false;
        }

        /// <summary>
        /// 字符串转Decimal类型，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string val)
        {
            if (IsBlank(val))
                return 0;
            decimal k;
            return decimal.TryParse(val, out k) ? k : 0;
        }

        /// <summary>
        /// 字符串转Double类型，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double ToDouble(this string val)
        {
            if (IsBlank(val))
                return 0;
            double k;
            return double.TryParse(val, out k) ? k : 0;
        }

        /// <summary>
        /// 字符串转float类型，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float ToFloat(this string val)
        {
            if (IsBlank(val))
                return 0;
            float k;
            return float.TryParse(val, out k) ? k : 0;
        }

        /// <summary>
        /// 字符串转数字，未转换成功返回0
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToIntWithDefaultValue(this string val, int defaultValue = 0)
        {
            if (IsBlank(val))
                return 0;
            int k;
            return int.TryParse(val, out k) ? k : defaultValue;
        }

        /// <summary>
        /// 将对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T obj) where T : class
        {
            return ToXml(obj, Encoding.Default.BodyName);
        }

        /// <summary>
        /// 将对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="encodeName"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T obj, string encodeName) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj", "obj is null.");

            if (obj is string) throw new ArgumentException("obj can't be string object.");

            var en = Encoding.GetEncoding(encodeName);
            var serial = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var ms = new MemoryStream();
            var xt = new XmlTextWriter(ms, en);
            serial.Serialize(xt, obj, ns);
            xt.Close();
            ms.Close();
            return en.GetString(ms.ToArray());
        }

        #endregion

        #region 字符验证

        /// <summary>
        /// 用于判断是否为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBlank(this string s)
        {
            return s == null || (s.Trim().Length == 0);
        }

        /// <summary>
        /// 用于判断是否为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotBlank(this string s)
        {
            return !s.IsBlank();
        }

        /// <summary>
        /// 验证QQ格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsQq(this string s)
        {
            return s.IsBlank() || Regex.IsMatch(s, @"^[1-9]\d{4,15}$");
        }

        /// <summary>
        /// 判断是否为有效的Email地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            if (!s.IsBlank())
            {
                const string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
                return Regex.IsMatch(s, pattern);
            }
            return false;
        }

        /// <summary>
        /// 验证是否是合法的电话号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhone(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^\+?((\d{2,4}(-)?)|(\(\d{2,4}\)))*(\d{0,16})*$");
            }
            return true;
        }

        /// <summary>
        /// 验证是否是合法的手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsMobile(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^(0|86|17951)?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
            }
            return false;
        }

        /// <summary>
        /// 是否是IP地址
        /// </summary>
        public static bool IsIpAddress(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$");
            }
            return false;
        }

        /// <summary>
        /// 是否身份证号，验证如下3种情况：
        /// 1.身份证号码为15位数字；
        /// 2.身份证号码为18位数字；
        /// 3.身份证号码为17位数字+1个字母
        /// </summary>
        public static bool IsIdentityCardId(this string value)
        {
            if (value.Length != 15 && value.Length != 18)
            {
                return false;
            }
            Regex regex;
            string[] array;
            DateTime time;
            if (value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})_");
                if (!regex.Match(value).Success)
                {
                    return false;
                }
                array = regex.Split(value);
                return DateTime.TryParse(string.Format("{0}-{1}-{2}", "19" + array[2], array[3], array[4]), out time);
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(value).Success)
            {
                return false;
            }
            array = regex.Split(value);
            if (!DateTime.TryParse(string.Format("{0}-{1}-{2}", array[2], array[3], array[4]), out time))
            {
                return false;
            }
            //校验最后一位
            string[] chars = value.ToCharArray().Select(m => m.ToString()).ToArray();
            int[] weights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                int num = int.Parse(chars[i]);
                sum = sum + num * weights[i];
            }
            int mod = sum % 11;
            string vCode = "10X98765432";//检验码字符串
            string last = vCode.ToCharArray().ElementAt(mod).ToString();
            return chars.Last().ToUpper() == last;
        }

        /// <summary>
        /// 验证是否是合法的邮编
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"[1-9]\d{5}(?!\d)");
            }
            return true;
        }

        /// <summary>
        /// 验证是否是合法的传真
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsFax(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
            }
            return true;
        }

        /// <summary>
        /// 是否Url字符串
        /// </summary>
        public static bool IsUrl(this string value)
        {
            try
            {
                if (value.IsBlank() || value.Contains(' '))
                {
                    return false;
                }
                Uri uri = new Uri(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查字符串是否为有效的int数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt(this string val)
        {
            if (IsBlank(val))
                return false;
            int k;
            return int.TryParse(val, out k);
        }

        /// <summary>
        /// 检查字符串是否为有效的INT64数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt64(this string val)
        {
            if (IsBlank(val))
                return false;
            long k;
            return long.TryParse(val, out k);
        }

        /// <summary>
        /// 检查字符串是否为有效的Decimal
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string val)
        {
            if (IsBlank(val))
                return false;
            decimal d;
            return decimal.TryParse(val, out d);
        }

        #endregion

        #region 其他

        /// <summary>
        /// 将XML字符串反序列化成对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T Deserial<T>(this string s) where T : class
        {
            return Deserial<T>(s, Encoding.Default.BodyName);
        }

        /// <summary>
        /// Deserial
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="encodeName"></param>
        /// <returns></returns>
        public static T Deserial<T>(this string s, string encodeName) where T : class
        {
            if (s.IsBlank())
            {
                throw new ApplicationException("xml string is null or empty.");
            }
            var serial = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            return (T)serial.Deserialize(new StringReader(s));
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetExt(this string s)
        {
            var ret = string.Empty;
            if (!s.Contains('.')) return ret;
            var temp = s.Split('.');
            ret = temp[temp.Length - 1];

            return ret;
        }

        /// <summary>
        /// 将驼峰字符串的第一个字符小写
        /// </summary>
        public static string LowerFirstChar(this string str)
        {
            if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
            {
                return str;
            }
            if (str.Length == 1)
            {
                return char.ToLower(str[0]).ToString();
            }
            return char.ToLower(str[0]) + str.Substring(1, str.Length - 1);
        }

        /// <summary>
        /// 将小驼峰字符串的第一个字符大写
        /// </summary>
        public static string UpperFirstChar(this string str)
        {
            if (string.IsNullOrEmpty(str) || !char.IsLower(str[0]))
            {
                return str;
            }
            if (str.Length == 1)
            {
                return char.ToUpper(str[0]).ToString();
            }
            return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
        }

        /// <summary>
        /// 如果为空，则返回默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string WithDefaultValueIfEmpty(this string value, string defaultValue)
        {
            return value.IsBlank() ? defaultValue : value;
        }

        #endregion
    }
}
