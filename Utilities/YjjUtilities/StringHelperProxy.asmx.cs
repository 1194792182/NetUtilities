using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web.Services;
using System.Web.Services.Protocols;
using YjjUtilities.StringUtilties;

namespace YjjUtilities
{
    /// <summary>
    ///     StringHelperProxy 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.1194792182.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class StringHelperProxy : WebService
    {
        public static readonly StringHelperSoapClient StringHelperSoapClient = new StringHelperSoapClient();

        private static readonly StringHelperSoapHeader StringHelperSoapHeader = new StringHelperSoapHeader() { SecretKey = "4965E71976130A3A469123DE8F97C7FD" };

        public readonly StringHelperProxySoapHeader StringHelperProxySoapHeader = new StringHelperProxySoapHeader();

        public StringHelperProxy()
        {
            ServicePointManager.Expect100Continue = false;
        }

        private StringHelperSoapClient CurrentClient
        {
            get { return StringHelperSoapClient; }
        }

        public StringHelperSoapHeader CurrentHeader
        {
            get
            {
                return StringHelperSoapHeader;
            }
        }

        #region 字符串转列表

        [WebMethod(Description = "将含有指定字符的字符串转成以该字符分割的整型列表")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public List<int> GetIntListBySplitStr(string splitStr, char spliter, bool isDistinct = false)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetIntListBySplitStr(CurrentHeader, splitStr, spliter, isDistinct).ToList();
        }

        [WebMethod(Description = "将含有指定字符的字符串转成以该字符分割的字符串列表")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public List<string> GetStringListBySplitStr(string splitStr, char spliter, bool isDistinct = false)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetStringListBySplitStr(CurrentHeader, splitStr, spliter, isDistinct).ToList();
        }

        [WebMethod(Description = "将含有指定正则表达式模式的字符串转成以该正则表达式模式匹配字符分割的字符串列表")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public List<string> GetSplitStrByRegexPatternResult(string sourceStr, string patternStr, bool isDistinct = false)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetSplitStrByRegexPatternResult(CurrentHeader,sourceStr, patternStr, isDistinct).ToList();
        }

        #endregion

        #region 列表转字符串

        [WebMethod(Description = "将字符串列表转成用指定字符分割的字符串")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string GetStrByStrListWithSpliter(List<string> strList, string spliter)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetStrByStrListWithSpliter(CurrentHeader, strList.ToArray(), spliter);
        }

        [WebMethod(Description = "将整型列表转成用指定字符分割的字符串")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string GetStrByIntListWithSpliter(List<int> intList, string spliter)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetStrByIntListWithSpliter(CurrentHeader, intList.ToArray(), spliter);
        }

        #endregion

        #region 全半角转换

        [WebMethod(Description = "转全角")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string ToFullWidthStr(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.ToFullWidthStr(CurrentHeader, input);
        }

        [WebMethod(Description = "转半角")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string ToHalfWidthStr(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.ToHalfWidthStr(CurrentHeader, input);
        }


        #endregion

        #region 字符串替换

        [WebMethod(Description = "替换指定字符串")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string ReplaceSpecialStr(string sourceStr, string specialStr)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.ReplaceSpecialStr(CurrentHeader, sourceStr, specialStr);
        }

        #endregion

        #region 加密字符串

        [WebMethod(Description = "获取加密后的字符串")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public string GetEncryptStrResult(string str, string encodingStr = "utf8")
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return null;
            }
            return CurrentClient.GetEncryptStrResult(CurrentHeader, str, encodingStr);
        }

        #endregion

        #region 验证输入

        [WebMethod(Description = "是否数字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsNum(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsNum(CurrentHeader, input);
        }

        [WebMethod(Description = "是否指定至多最大位数的数字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsMaximumLengthNum(string input, int length)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsMaximumLengthNum(CurrentHeader, input, length);
        }

        [WebMethod(Description = "是否指定至少最小位数的数字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsMinimumLengthNum(string input, int length)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsMinimumLengthNum(CurrentHeader, input, length);
        }

        [WebMethod(Description = "是否指定范围长度的数字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsBetweenLengthNum(string input, int min, int max)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsBetweenLengthNum(CurrentHeader, input, min, max);
        }

        [WebMethod(Description = "是否有两位小数的正实数")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsPositiveRealNumWithTwoDecimalPlaces(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsPositiveRealNumWithTwoDecimalPlaces(CurrentHeader, input);
        }

        [WebMethod(Description = "是否有1~3位小数的正实数")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsPositiveRealNumWithZeroToThreeDecimalPlaces(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsPositiveRealNumWithZeroToThreeDecimalPlaces(CurrentHeader, input);
        }


        /// <summary>
        /// 非零正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "非零正整数")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsNonZeroPositiveInteger(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsNonZeroPositiveInteger(CurrentHeader, input);
        }

        /// <summary>
        /// 非零负整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "非零负整数")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsNonZeroNegativeInteger(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsNonZeroNegativeInteger(CurrentHeader, input);
        }

        /// <summary>
        /// 是否指定长度的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的字符串")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsSpecifiedLengthStr(string input, int length)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsSpecifiedLengthStr(CurrentHeader, input, length);
        }

        /// <summary>
        /// 是否指定长度的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的数字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsSpecifiedLengthNumStr(string input, int length)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsSpecifiedLengthNumStr(CurrentHeader, input, length);
        }

        /// <summary>
        /// 是否英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否英文字母")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsLetter(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsLetter(CurrentHeader, input);
        }

        /// <summary>
        /// 是否大写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否大写英文字母")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsCapitalLetter(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsCapitalLetter(CurrentHeader, input);
        }

        /// <summary>
        /// 是否小写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否小写英文字母")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsLowerCaseLetter(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsLowerCaseLetter(CurrentHeader, input);
        }

        /// <summary>
        /// 是否数字和字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否数字和字母")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsNumAndLetter(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsNumAndLetter(CurrentHeader, input);
        }

        /// <summary>
        /// 是否数字、字母和下划线
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否数字、字母和下划线")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsNumAndLetterAndUnderline(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsNumAndLetterAndUnderline(CurrentHeader, input);
        }

        /// <summary>
        /// 是否指定长度的用户名
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否指定长度的用户名")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsUserNameWithSpecifiedLength(string input, int min = 4, int max = 15)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsUserNameWithSpecifiedLength(CurrentHeader, input, min, max);
        }

        /// <summary>
        /// 是否汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否汉字")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsChineseCharacter(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsChineseCharacter(CurrentHeader, input);
        }

        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否身份证")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsIdNumStr(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsIdNumStr(CurrentHeader, input);
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "是否电子邮件")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsEmail(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsEmail(CurrentHeader, input);
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否手机号码")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsPhone(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsPhone(CurrentHeader, input);
        }

        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否电话号码")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsTel(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsTel(CurrentHeader, input);
        }

        /// <summary>
        /// 是否邮政编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否邮政编码")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsPostalCode(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsPostalCode(CurrentHeader, input);
        }

        /// <summary>
        /// 是否qq
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否qq")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsQq(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsQq(CurrentHeader, input);
        }

        /// <summary>
        /// 是否ip地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [WebMethod(Description = "是否ip地址")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsIpAddress(string input)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsIpAddress(CurrentHeader, input);
        }

        #endregion

        #region 正则表达式验证

        [WebMethod(Description = "是否与指定正则表达式匹配")]
        [SoapHeader("StringHelperProxySoapHeader")]
        public bool IsMatch(string input, string pattern, int options = 0)
        {
            if (!StringHelperProxySoapHeader.IsValid)
            {
                return false;
            }
            return CurrentClient.IsMatch(CurrentHeader, input, pattern, options);
        }

        #endregion
    }
}