using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Utility
{

    /// <summary>
    /// 加密/解密
    /// </summary>
    public class EncryptUtility
    {
        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptMD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }
        /// <summary>
        /// 密码md5加密方式
        /// </summary>
        /// <param name="key">加盐，防止md5暴力破解</param>
        /// <param name="pwd">密码字符串</param>
        /// <returns></returns>
        public static string PwdToMD5(string pwd,string key)
        {
            return EncryptMD5(EncryptMD5(pwd) + key);
        }


    }
}
