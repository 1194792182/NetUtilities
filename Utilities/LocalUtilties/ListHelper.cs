using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LocalUtilties.YjjUtilities;

namespace LocalUtilties
{
    public static class ListHelper
    {
        private static readonly StringHelperProxySoapClient StrHelperClient;
        private static readonly string SecretKey = "4965E71976130A3A469123DE8F97C7FD";
        static ListHelper()
        {
            StrHelperClient = new StringHelperProxySoapClient();
            ServicePointManager.Expect100Continue = false;
        }

        #region 列表转字符串
        /// <summary>
        /// 将字符串列表转成用指定字符分割的字符串
        /// </summary>
        /// <param name="list">字符串列表</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string ToStrWithSpliter(this List<string> list, string spliter)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetStrByStrListWithSpliter(header, list.ToArray(), spliter);
        }

        /// <summary>
        /// 将整型列表转成用指定字符分割的字符串
        /// </summary>
        /// <param name="list">整型列表</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string ToStrWithSpliter(this List<int> list, string spliter)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetStrByIntListWithSpliter(header, list.ToArray(), spliter);
        }

        #endregion
    }
}
