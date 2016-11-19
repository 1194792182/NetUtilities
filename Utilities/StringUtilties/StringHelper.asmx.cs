using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace StringUtilties
{
    /// <summary>
    ///     StringHelper 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.1194792182.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class StringHelper : WebService
    {
        public readonly StringHelperSoapHeader StringHelperSoapHeader = new StringHelperSoapHeader();
        
        #region 字符串转列表

        /// <summary>
        /// 将含有指定字符的字符串转成以该字符分割的指定类型列表
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="splitStr">有分割字符的字符串</param>
        /// <param name="spliter">分割字符</param>
        /// <returns>List</returns>
        private List<T> GetListBySplitStr<T>(string splitStr, char spliter)
        {
            var strArr = splitStr.Split(spliter);
            var list = (from item in strArr
                        where !string.IsNullOrEmpty(item) && !item.Equals(spliter.ToString())
                        select (T)Convert.ChangeType(item, typeof(T))).ToList();
            return list;
        }

        private class IntCompare:  IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }

        private class StringCompare : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        /// <summary>
        /// 将含有指定字符的字符串转成以该字符分割的整型列表
        /// </summary>
        /// <param name="splitStr">有分割字符的字符串</param>
        /// <param name="spliter">分割字符</param>
        /// <param name="isDistinct">是否去重复</param>
        /// <returns>List</returns>
        [WebMethod(Description = "将含有指定字符的字符串转成以该字符分割的整型列表")]
        [SoapHeader("StringHelperSoapHeader")]
        public List<int> GetIntListBySplitStr(string splitStr, char spliter, bool isDistinct = false)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            var list = GetListBySplitStr<int>(splitStr, spliter);

            if (isDistinct)
            {
                list = list.Distinct(new IntCompare()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 将含有指定字符的字符串转成以该字符分割的字符串列表
        /// </summary>
        /// <param name="splitStr">有分割字符的字符串</param>
        /// <param name="spliter">分割字符</param>
        /// <param name="isDistinct">是否去除重复</param>
        /// <returns>List</returns>
        [WebMethod(Description = "将含有指定字符的字符串转成以该字符分割的字符串列表")]
        [SoapHeader("StringHelperSoapHeader")]
        public List<string> GetStringListBySplitStr(string splitStr, char spliter, bool isDistinct = false)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            var list=GetListBySplitStr<string>(splitStr, spliter);
            
            if (isDistinct)
            {
                list = list.Distinct(new StringCompare()).ToList();
            }

            return list;
        }

        /// <summary>
        /// 将含有指定正则表达式模式的字符串转成以该正则表达式模式匹配字符分割的字符串列表
        /// </summary>
        /// <param name="sourceStr">要分割的字符串</param>
        /// <param name="patternStr">正则表达式模式的字符串</param>
        /// <param name="isDistinct">是否去除重复</param>
        /// <returns></returns>
        [WebMethod(Description = "将含有指定正则表达式模式的字符串转成以该正则表达式模式匹配字符分割的字符串列表")]
        [SoapHeader("StringHelperSoapHeader")]
        public List<string> GetSplitStrByRegexPatternResult(string sourceStr, string patternStr, bool isDistinct = false)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            var list = new List<string>();
            if (!string.IsNullOrEmpty(sourceStr))
            {
                list = new Regex(patternStr).Split(sourceStr).ToList();
                list = list.Where(q => !string.IsNullOrEmpty(q)).ToList();
            }

            if (isDistinct)
            {
                list = list.Distinct(new StringCompare()).ToList();
            }

            return list;
        }

        #endregion

        #region 列表转字符串
        /// <summary>
        /// 将列表转成用指定字符分割的字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">类型列表</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        private string GetStrByListWithSpliter<T>(List<T> list, string spliter)
        {
            var strB = new StringBuilder();
            foreach (var item in list)
            {
                if (item.Equals(list.LastOrDefault()))
                {
                    strB.Append(item);
                }
                else
                {
                    strB.AppendFormat(item + "{0}", spliter);
                }
            }
            return strB.ToString();
        }

        /// <summary>
        /// 将字符串列表转成用指定字符分割的字符串
        /// </summary>
        /// <param name="strList">字符串列表</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        [WebMethod(Description = "将字符串列表转成用指定字符分割的字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string GetStrByStrListWithSpliter(List<string> strList, string spliter)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            return GetStrByListWithSpliter(strList, spliter);
        }

        /// <summary>
        /// 将整型列表转成用指定字符分割的字符串
        /// </summary>
        /// <param name="intList">整型列表</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        [WebMethod(Description = "将整型列表转成用指定字符分割的字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string GetStrByIntListWithSpliter(List<int> intList, string spliter)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            return GetStrByListWithSpliter(intList, spliter);
        }

        #endregion

        #region 全半角转换

        /// <summary>
        /// 转全角
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        [WebMethod(Description = "转全角")]
        [SoapHeader("StringHelperSoapHeader")]
        public string ToFullWidthStr(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            var charArr = input.ToCharArray();
            for (var i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == 32)
                {
                    charArr[i] = (char)12288;
                    continue;
                }
                if (charArr[i] < 127)
                    charArr[i] = (char)(charArr[i] + 65248);
            }
            return new string(charArr);
        }

        /// <summary>
        /// 转半角
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        [WebMethod(Description = "转半角")]
        [SoapHeader("StringHelperSoapHeader")]
        public string ToHalfWidthStr(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            var charArr = input.ToCharArray();
            for (var i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == 12288)
                {
                    charArr[i] = (char)32;
                    continue;
                }
                if (charArr[i] > 65280 && charArr[i] < 65375)
                    charArr[i] = (char)(charArr[i] - 65248);
            }
            return new string(charArr);
        }


        #endregion

        #region 字符串替换

        /// <summary>
        /// 替换指定字符串
        /// </summary>
        /// <param name="sourceStr">源字符</param>
        /// <param name="specialStr">指定字符串</param>
        /// <returns></returns>
        [WebMethod(Description = "替换指定字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string ReplaceSpecialStr(string sourceStr,string specialStr)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            string returnValue;
            if (string.IsNullOrEmpty(sourceStr))
            {
                returnValue = string.Empty;
            }
            else
            {
                var targetStr = sourceStr.Replace(specialStr,string.Empty);
                returnValue = targetStr;
            }
            return returnValue;
        }

        #endregion

        #region 加密字符串

        private Encoding GetEncodingByStr(string encodingStr=null)
        {
            var encoding = Encoding.UTF8;
            if (string.IsNullOrEmpty(encodingStr))
            {
                return encoding;
            }
            switch (encodingStr.ToUpper())
            {
                case "UTF8":
                    encoding = Encoding.UTF8;
                    break;
                case "ASCII":
                    encoding = Encoding.ASCII;
                    break;
                case "UTF32":
                    encoding = Encoding.UTF32;
                    break;
                case "UTF7":
                    encoding = Encoding.UTF7;
                    break;
                case "Unicode":
                    encoding = Encoding.Unicode;
                    break;
                default:
                    encoding = Encoding.Default;
                    break;
            }

            return encoding;
        }

        private string GetToSha1Result(string sourceStr, string encodingStr = "utf8")
        {
            var cleanBytes = GetEncodingByStr(encodingStr).GetBytes(sourceStr);
            var hashedBytes = SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }

        private string GetToMd5Result(string sourceStr, string encodingStr = "utf8")
        {
            var cleanBytes = GetEncodingByStr(encodingStr).GetBytes(sourceStr);
            var hashedBytes = MD5.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }

        /// <summary>
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <param name="encodingStr">编码字符串（全小写）</param>
        /// <returns></returns>
        [WebMethod(Description = "获取加密后的字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string GetEncryptStrResult(string str, string encodingStr = "utf8")
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return null;
            }
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            str = GetToSha1Result(str, encodingStr);
            str = GetToMd5Result(str, encodingStr);
            return str;
        }

        /// <summary>
        /// 获取使用加盐Md5加密的字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        [WebMethod(Description = "获取使用加盐Md5加密的字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string GetMd5SaltStrResult(string str, string salt)
        {
            var sourceStr = string.Concat(str, salt);
            sourceStr = string.Concat(salt, sourceStr, salt, sourceStr);
            var result = GetToMd5Result(sourceStr);
            return result;
        }

        #endregion

        #region 验证输入
        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否数字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsNum(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[0-9]*$");
        }

        /// <summary>
        /// 只能输入指定位数的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定至多最大位数的数字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsMaximumLengthNum(string input,int length)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\d{" + length + "}$");
        }

        /// <summary>
        /// 输入至少指定位数的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定至少最小位数的数字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsMinimumLengthNum(string input, int length)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\d{" + length + ",}$");
        }

        /// <summary>
        /// 只能输入指定范围长度的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定范围长度的数字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsBetweenLengthNum(string input, int min,int max)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\d{" + min + "," + max + "}$");
        }

        /// <summary>
        /// 只能输入有两位小数的正实数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否有两位小数的正实数")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsPositiveRealNumWithTwoDecimalPlaces(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[0-9]+(\.[0-9]{2})?$");
        }

        /// <summary>
        /// 只能输入有1~3位小数的正实数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否有1~3位小数的正实数")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsPositiveRealNumWithZeroToThreeDecimalPlaces(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[0-9]+(\.[0-9]{1,3})?$");
        }

        /// <summary>
        /// 非零正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "非零正整数")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsNonZeroPositiveInteger(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\+?[1-9][0-9]*$");
        }

        /// <summary>
        /// 非零负整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "非零负整数")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsNonZeroNegativeInteger(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\-[1-9][0-9]*$");
        }

        /// <summary>
        /// 是否指定长度的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsSpecifiedLengthStr(string input, int length)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^.{" + length + "}$");
        }

        /// <summary>
        /// 是否指定长度的数字字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的数字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsSpecifiedLengthNumStr(string input, int length)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\d{" + length + "}$");
        }

        /// <summary>
        /// 是否英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否英文字母")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsLetter(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 是否大写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否大写英文字母")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsCapitalLetter(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[A-Z]+$");
        }

        /// <summary>
        /// 是否小写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否小写英文字母")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsLowerCaseLetter(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[a-z]+$");
        }

        /// <summary>
        /// 是否数字和字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否数字和字母")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsNumAndLetter(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 是否数字、字母和下划线
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否数字、字母和下划线")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsNumAndLetterAndUnderline(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\w+$");
        }

        /// <summary>
        /// 是否指定长度的用户名
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的用户名")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsUserNameWithSpecifiedLength(string input, int min = 4, int max = 15)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[a-zA-Z]\w{" + min + "," + max + "}$");
        }

        /// <summary>
        /// 是否汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否汉字")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsChineseCharacter(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[\u4e00-\u9fa5]*$");
        }

        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否身份证")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsIdNumStr(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$")
                || Regex.IsMatch(input, @"^[1-9]\d{5}\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}$");
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "是否电子邮件")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsEmail(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$");
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否手机号码")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsPhone(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^1[3|4|5|7|8][0-9]\d{8}$");
        }

        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否电话号码")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsTel(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^((\d{3,4})|(\d{3,4}-))?\d{7,8}$");
        }

        /// <summary>
        /// 是否邮政编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否邮政编码")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsPostalCode(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[1-9]\d{5}$");
        }
        
        /// <summary>
        /// 是否qq
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否qq")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsQq(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^[1-9][0-9]{4,}$");
        }

        /// <summary>
        /// 是否ip地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否ip地址")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsIpAddress(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }

        #endregion

        #region 正则表达式验证
        /// <summary>
        /// 是否与指定正则表达式匹配
        /// </summary>
        /// <param name="input">要匹配的内容</param>
        /// <param name="pattern">正则表达式字符串</param>
        /// <param name="options">正则表达式选项</param>
        /// <returns></returns>
        [WebMethod(Description = "是否与指定正则表达式匹配")]
        [SoapHeader("StringHelperSoapHeader")]
        public bool IsMatch(string input, string pattern , int options = 0)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return false;
            }
            return Regex.IsMatch(input, pattern, (RegexOptions)options);
        }

        #endregion

        #region 根据字符串长度处理字符串

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="input">要处理的字符串</param>
        /// <returns></returns>
        [WebMethod(Description = "得到字符串长度，一个汉字长度为2")]
        [SoapHeader("StringHelperSoapHeader")]
        public int GetStrLength(string input)
        {
            if (!StringHelperSoapHeader.IsValid)
            {
                return -1;
            }
            var ascii = new ASCIIEncoding();
            var length = 0;
            var bytes = ascii.GetBytes(input);
            foreach (var t in bytes)
            {
                if (t == 63)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        /// <summary>
        /// 按指定的长度截取字符串
        /// </summary>
        /// <param name="input">要截取的字符串</param>
        /// <param name="length">要截取的长度</param>
        /// <param name="fixStr">截取后填充的字符</param>
        /// <returns></returns>
        [WebMethod(Description = "按指定的长度截取字符串")]
        [SoapHeader("StringHelperSoapHeader")]
        public string GetSubStrByLength(string input, int length, string fixStr = "...")
        {
            var showFixStr = false;

            if (length % 2 == 1)
            {
                showFixStr = true;
                length--;
            }

            var tempLength = 0;
            var tempStr = string.Empty;
            var bytes = new ASCIIEncoding().GetBytes(input);

            for (var i = 0; i < bytes.Length; i++)
            {
                var step = 0;
                step = bytes[i] == 63 ? 2 : 1;

                tempLength += step;

                try
                {
                    tempStr += input.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLength >= length)
                {
                    break;
                }
            }

            var inputByte = Encoding.Default.GetBytes(input);
            if (showFixStr && inputByte.Length > length)
            {
                tempStr += fixStr;
            }
            return tempStr;
        }


        #endregion


    }
}