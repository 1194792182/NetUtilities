using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LocalUtilties.YjjUtilities;


namespace LocalUtilties
{
    public static class StringHelper
    {
        private static readonly StringHelperProxySoapClient StrHelperClient;
        private static readonly string SecretKey = "4965E71976130A3A469123DE8F97C7FD";
        static StringHelper()
        {
            StrHelperClient = new StringHelperProxySoapClient();
            ServicePointManager.Expect100Continue = false;
        }

        #region 字符串转列表
        /// <summary>
        /// 将含有指定字符的字符串转成以该字符分割的整型列表
        /// </summary>
        /// <param name="splitStr">有分割字符的字符串</param>
        /// <param name="spliter">分割字符</param>
        /// <param name="isDistinct">是否去重复</param>
        /// <returns>List</returns>
        public static List<int> ToIntList(this string splitStr, char spliter, bool isDistinct = false)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetIntListBySplitStr(header, splitStr, spliter, isDistinct).ToList();
        }

        /// <summary>
        /// 将含有指定字符的字符串转成以该字符分割的字符串列表
        /// </summary>
        /// <param name="splitStr">有分割字符的字符串</param>
        /// <param name="spliter">分割字符</param>
        /// <param name="isDistinct">是否去除重复</param>
        /// <returns>List</returns>
        public static List<string> ToStrList(this string splitStr, char spliter, bool isDistinct = false)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetStringListBySplitStr(header, splitStr, spliter, isDistinct).ToList();
        }

        /// <summary>
        /// 将含有指定正则表达式模式的字符串转成以该正则表达式模式匹配字符分割的字符串列表
        /// </summary>
        /// <param name="sourceStr">要分割的字符串</param>
        /// <param name="patternStr">正则表达式模式的字符串</param>
        /// <param name="isDistinct">是否去除重复</param>
        /// <returns></returns>
        public static List<string> ToStrListByPattern(this string sourceStr, string patternStr, bool isDistinct = false)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetSplitStrByRegexPatternResult(header,sourceStr, patternStr, isDistinct).ToList();
        }

        #endregion

        #region 全半角转换
        /// <summary>
        /// 转全角
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string ToFullWidthStr(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.ToFullWidthStr(header,input);
        }

        /// <summary>
        /// 转半角
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string ToHalfWidthStr(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.ToHalfWidthStr(header,input);
        }

        #endregion

        #region 字符串替换

        /// <summary>
        /// 替换指定字符串
        /// </summary>
        /// <param name="input">源字符</param>
        /// <param name="specialStr">指定字符串</param>
        /// <returns></returns>
        public static string ReplaceSpecialStr(this string input,string specialStr)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.ReplaceSpecialStr(header, input, specialStr);
        }

        #endregion

        #region 加密字符串
        /// <summary>
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="password">需要加密的字符串</param>
        /// <param name="encodingStr">编码字符串（全小写）</param>
        /// <returns></returns>
        public static string ToPasswordStr(this string password, string encodingStr = "utf8")
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetEncryptStrResult(header,password, encodingStr);
        }

        /// <summary>
        /// 获取使用加盐Md5加密的字符串
        /// </summary>
        /// <param name="pwd">要加密的字符串</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        public static string ToMd5SaltPwdStr(this string pwd, string salt)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.GetMd5SaltStrResult(header, pwd, salt);
        }

        #endregion

        #region 验证输入
        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public static bool ValidateNum(this string numStr)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsNum(header,numStr);
        }
        /// <summary>
        /// 只能输入指定位数的数字
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool ValidateNumMax(this string numStr,int maxLength)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsMaximumLengthNum(header,numStr, maxLength);
        }

        /// <summary>
        /// 输入至少指定位数的数字
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public static bool ValidateNumMin(this string numStr,int minLength)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsMinimumLengthNum(header,numStr, minLength);
        }

        /// <summary>
        /// 只能输入指定范围长度的数字
        /// </summary>
        /// <param name="numStr"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool ValidateNumBetweenLength(this string numStr, int min,int max)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsBetweenLengthNum(header,numStr, min, max);
        }

        /// <summary>
        /// 只能输入有两位小数的正实数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPositiveRealNumWithTwoDecimalPlaces(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsPositiveRealNumWithTwoDecimalPlaces(header,input);
        }

        /// <summary>
        /// 只能输入有1~3位小数的正实数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPositiveRealNumWithOneToThreeDecimalPlaces(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsPositiveRealNumWithZeroToThreeDecimalPlaces(header,input);
        }

        /// <summary>
        /// 非零正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNonZeroPositiveInteger(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsNonZeroPositiveInteger(header,input);
        }

        /// <summary>
        /// 非零负整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNonZeroNegativeInteger(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsNonZeroNegativeInteger(header,input);
        }

        /// <summary>
        /// 是否指定长度的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsSpecifiedLengthStr(this string input,int length)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsSpecifiedLengthStr(header,input, length);
        }

        /// <summary>
        /// 是否指定长度的数字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsSpecifiedLengthNumStr(this string input, int length)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsSpecifiedLengthNumStr(header,input, length);
        }

        /// <summary>
        /// 是否英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLetter(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsLetter(header,input);
        }

        /// <summary>
        /// 是否大写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsCapitalLetter(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsCapitalLetter(header, input);
        }

        /// <summary>
        /// 是否小写英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLowerCaseLetter(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsLowerCaseLetter(header,input);
        }

        /// <summary>
        /// 是否数字和字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumAndLetter(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsNumAndLetter(header,input);
        }

        /// <summary>
        /// 是否数字、字母和下划线
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumAndLetterAndUnderline(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsNumAndLetterAndUnderline(header,input);
        }

        /// <summary>
        /// 是否指定长度的用户名
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsUserNameWithSpecifiedLength(this string input, int min = 4, int max = 15)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsUserNameWithSpecifiedLength(header,input, min, max);
        }

        /// <summary>
        /// 是否汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseCharacter(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsChineseCharacter(header,input);
        }

        /// <summary>
        /// 是否身份证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIdNumStr(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsIdNumStr(header,input);
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        /// <returns></returns>
        public static bool IsEmail(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsEmail(header,input);
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsPhone(header,input);
        }

        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsTel(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsTel(header,input);
        }

        /// <summary>
        /// 是否邮政编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPostalCode(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsPostalCode(header,input);
        }

        /// <summary>
        /// 是否qq
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsQq(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsQq(header,input);
        }

        /// <summary>
        /// 是否ip地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIpAddress(this string input)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsIpAddress(header,input);
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
        public static bool IsMatch(this string input, string pattern, int options = 0)
        {
            var header = new StringHelperProxySoapHeader() { SecretKey = SecretKey };
            return StrHelperClient.IsMatch(header, input, pattern, options);
        }

        #endregion
    }
}
