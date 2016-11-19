using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LocalUtilties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilitiesTest
{
    /// <summary>
    ///     StringHelperUnitTest 的摘要说明
    /// </summary>
    [TestClass]
    public class StringHelperUnitTest
    {
        #region 列表转字符串

        /// <summary>
        /// 测试列表转字符串
        /// </summary>
        [TestMethod]
        public void TestToStrWithSpliter()
        {
            var strList = new List<string> {"11", "12"};
            var result = strList.ToStrWithSpliter(",");
            Assert.IsTrue(result == "11,12", "result=='11,12'");

            var intList = new List<int> {11, 12};
            result = intList.ToStrWithSpliter(",");
            Assert.IsTrue(result == "11,12", "result=='11,12'");

            result = intList.ToStrWithSpliter(string.Empty);
            Assert.IsTrue(result == "1112", "result == '1112'");
        }

        #endregion

        #region 字符串替换
        /// <summary>
        /// 测试字符串替换
        /// </summary>
        [TestMethod]
        public void TestReplaceSpecialStr()
        {
            var sourceStr = "abcdefabcdefabc";
            var specialStr = "abc";
            Assert.IsTrue(sourceStr.ReplaceSpecialStr(specialStr).Equals("defdef"),
                "sourceStr.ReplaceSpecialStr(specialStr).Equals('defdef')");
        }

        #endregion

        #region 加密字符串
        /// <summary>
        /// 测试加密字符串
        /// </summary>
        [TestMethod]
        public void TestToPasswordStr()
        {
            var password = "123456";
            var encryptResult = password.ToPasswordStr("132g");
            Assert.IsTrue(!string.IsNullOrEmpty(encryptResult), "!string.IsNullOrEmpty(encryptResult)");
            Assert.IsTrue(encryptResult.Length == 32, "encryptResult.Length==32");
        }

        /// <summary>
        /// 测试MD5加盐加密字符串
        /// </summary>
        [TestMethod]
        public void TestToMd5SaltPwdStr()
        {
            var password = "123456";
            var firstEncryptResult = password.ToMd5SaltPwdStr("12$#");
            var secondEncryptResult = password.ToMd5SaltPwdStr(String.Empty);
            Assert.IsTrue(!firstEncryptResult.Equals(secondEncryptResult),"!firstEncryptResult.Equals(secondEncryptResult)");
        }

        #endregion

        #region 正则表达式验证
        /// <summary>
        /// 测试是否与指定正则表达式匹配
        /// </summary>
        [TestMethod]
        public void TestIsMatch()
        {
            var str = "Abc";
            Assert.IsTrue(str.IsMatch("abc", (int) RegexOptions.IgnoreCase),
                "str.IsMatch('abc', (int)RegexOptions.IgnoreCase)");
            Assert.IsTrue(str.IsMatch("^[a-z][a-z]c$", (int) RegexOptions.IgnoreCase),
                "str.IsMatch('^a[a-z]c$', (int)RegexOptions.IgnoreCase)");
            Assert.IsFalse(str.IsMatch("abc"), "str.IsMatch('abc')");
        }

        #endregion

        #region 字符串转列表
        /// <summary>
        /// 测试将含有指定字符的字符串转成以该字符分割的整型列表
        /// </summary>
        [TestMethod]
        public void TestToIntList()
        {
            var list = "11,12,11".ToIntList(',');
            Assert.IsTrue(list.Count == 3, "list.Count==3");
            Assert.IsTrue(list.Sum() == 34, "list.Sum()==34");
            list = "11,12,11".ToIntList(',', true);
            Assert.IsTrue(list.Count == 2, "list.Count==2");
            Assert.IsTrue(list.Sum() == 23, "list.Sum()==23");
        }
        /// <summary>
        /// 测试将含有指定字符的字符串转成以该字符分割的字符串列表
        /// </summary>
        [TestMethod]
        public void TestToStrList()
        {
            var list = "11,12,11".ToStrList(',');
            Assert.IsTrue(list.Count == 3, "list.Count==3");
            Assert.IsTrue(string.Join("|", list.ToArray()).Equals("11|12|11"),
                "string.Join('|', list.ToArray()).Equals('11|12|11')");
            list = "11,12,11".ToStrList(',', true);
            Assert.IsTrue(list.Count == 2, "list.Count==2");
            Assert.IsTrue(string.Join("|", list.ToArray()).Equals("11|12"),
                "string.Join('|', list.ToArray()).Equals('11|12')");
        }
        /// <summary>
        /// 测试将含有指定正则表达式模式的字符串转成以该正则表达式模式匹配字符分割的字符串列表
        /// </summary>
        [TestMethod]
        public void TestToStrListByPattern()
        {
            var list = "abc11ddabc22dd".ToStrListByPattern(@"\d+d+");
            Assert.IsTrue(list.Count == 2, "list.Count==2");
            Assert.IsTrue(string.Join("|", list.ToArray()).Equals("abc|abc"),
                "string.Join('|', list.ToArray()).Equals('abc|abc')");
            list = "abc11ddabc22dd".ToStrListByPattern(@"\d+d+", true);
            Assert.IsTrue(list.Count == 1, "list.Count==1");
            Assert.IsTrue(list[0] == "abc");
            list = "abc11ddcde22dd".ToStrListByPattern(@"\d+d+", true);
            Assert.IsTrue(list.Count == 2, "list.Count==2");
            Assert.IsTrue(string.Join("|", list.ToArray()).Equals("abc|cde"),
                "string.Join('|', list.ToArray()).Equals('abc|cde')");
        }

        #endregion

        #region 全半角转换
        /// <summary>
        /// 测试转全角
        /// </summary>
        [TestMethod]
        public void TestToFullWidthStr()
        {
            var testStr = "abc".ToFullWidthStr();
            Assert.IsTrue(testStr.Equals("ａｂｃ"));
        }

        /// <summary>
        /// 测试转半角
        /// </summary>
        [TestMethod]
        public void TestToHalfWidthStr()
        {
            var testStr = "ａｂｃ".ToHalfWidthStr();
            Assert.IsTrue(testStr.Equals("abc"));
        }

        #endregion

        #region 验证输入
        /// <summary>
        /// 测试只能输入数字
        /// </summary>
        [TestMethod]
        public void TestValidateNum()
        {
            var numStr = "aaa";
            Assert.IsTrue(!numStr.ValidateNum(), "!numStr.ValidateNum()");
            numStr = "12";
            Assert.IsTrue(numStr.ValidateNum(), "numStr.ValidateNum()");
        }

        /// <summary>
        /// 测试只能输入指定位数的数字
        /// </summary>
        [TestMethod]
        public void TestValidateNumMax()
        {
            var numStr = "111";
            Assert.IsTrue(numStr.ValidateNumMax(3), "numStr.ValidateNumMax(3)");
            Assert.IsTrue(!numStr.ValidateNumMax(2), "!numStr.ValidateNumMax(2)");
        }

        /// <summary>
        /// 测试输入至少指定位数的数字
        /// </summary>
        [TestMethod]
        public void TestValidateNumMin()
        {
            var numStr = "1";
            Assert.IsFalse(numStr.ValidateNumMin(2), "numStr.ValidateNumMin(2)");
            Assert.IsTrue(numStr.ValidateNumMin(1), "numStr.ValidateNumMin(1)");
        }

        /// <summary>
        /// 测试只能输入指定范围长度的数字
        /// </summary>
        [TestMethod]
        public void TestValidateNumBetweenLength()
        {
            var numStr = "12345";
            Assert.IsFalse(numStr.ValidateNumBetweenLength(2, 4), "numStr.ValidateNumBetweenLength(2, 4)");
            Assert.IsTrue(numStr.ValidateNumBetweenLength(2, 5), "numStr.ValidateNumBetweenLength(2, 5)");
        }

        /// <summary>
        /// 测试只能输入有两位小数的正实数
        /// </summary>
        [TestMethod]
        public void TestIsPositiveRealNumWithTwoDecimalPlaces()
        {
            var numStr = "1.234";
            Assert.IsFalse(numStr.IsPositiveRealNumWithTwoDecimalPlaces(),
                "numStr.IsPositiveRealNumWithTwoDecimalPlaces()");
            numStr = "1.23";
            Assert.IsTrue(numStr.IsPositiveRealNumWithTwoDecimalPlaces(),
                "numStr.IsPositiveRealNumWithTwoDecimalPlaces()");
            numStr = "1$23";
            Assert.IsFalse(numStr.IsPositiveRealNumWithTwoDecimalPlaces(),
                "numStr.IsPositiveRealNumWithTwoDecimalPlaces()");
            numStr = "1.3";
            Assert.IsFalse(numStr.IsPositiveRealNumWithTwoDecimalPlaces(),
                "numStr.IsPositiveRealNumWithTwoDecimalPlaces()");
        }

        /// <summary>
        /// 测试只能输入有1~3位小数的正实数
        /// </summary>
        [TestMethod]
        public void TestIsPositiveRealNumWithOneToThreeDecimalPlaces()
        {
            var numStr = "1.234";
            Assert.IsTrue(numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces(),
                "numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces()");
            numStr = "1.23";
            Assert.IsTrue(numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces(),
                "numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces()");
            numStr = "1$23";
            Assert.IsFalse(numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces(),
                "numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces()");
            numStr = "1.3";
            Assert.IsTrue(numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces(),
                "numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces()");
            numStr = "1";
            Assert.IsTrue(numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces(),
                "numStr.IsPositiveRealNumWithOneToThreeDecimalPlaces()");
        }

        /// <summary>
        /// 测试非零正整数
        /// </summary>
        [TestMethod]
        public void TestIsNonZeroPositiveInteger()
        {
            var numStr = "-1";

            Assert.IsFalse(numStr.IsNonZeroPositiveInteger(), "numStr.IsNonZeroPositiveInteger()");

            numStr = "1.0";

            Assert.IsFalse(numStr.IsNonZeroPositiveInteger(), "numStr.IsNonZeroPositiveInteger()");

            numStr = "1";

            Assert.IsTrue(numStr.IsNonZeroPositiveInteger(), "numStr.IsNonZeroPositiveInteger()");
        }

        /// <summary>
        /// 测试非零负整数
        /// </summary>
        [TestMethod]
        public void TestIsNonZeroNegativeInteger()
        {
            var numStr = "-1";

            Assert.IsTrue(numStr.IsNonZeroNegativeInteger(), "numStr.IsNonZeroNegativeInteger()");

            numStr = "-1.0";

            Assert.IsFalse(numStr.IsNonZeroNegativeInteger(), "numStr.IsNonZeroNegativeInteger()");

            numStr = "1";

            Assert.IsFalse(numStr.IsNonZeroNegativeInteger(), "numStr.IsNonZeroNegativeInteger()");
        }

        /// <summary>
        /// 测试是否指定长度的字符串
        /// </summary>
        [TestMethod]
        public void TestIsSpecifiedLengthStr()
        {
            var str = "abc";
            Assert.IsTrue(str.IsSpecifiedLengthStr(3), "str.IsSpecifiedLengthStr(3)");
            Assert.IsFalse(str.IsSpecifiedLengthStr(4), "str.IsSpecifiedLengthStr(4)");
        }

        /// <summary>
        /// 测试是否指定长度的数字字符串
        /// </summary>
        [TestMethod]
        public void TestIsSpecifiedLengthNumStr()
        {
            var str = "abc";
            Assert.IsFalse(str.IsSpecifiedLengthNumStr(3), "str.IsSpecifiedLengthNumStr(3)");
            str = "123";
            Assert.IsTrue(str.IsSpecifiedLengthNumStr(3), "str.IsSpecifiedLengthNumStr(3)");
            Assert.IsFalse(str.IsSpecifiedLengthNumStr(4), "str.IsSpecifiedLengthNumStr(4)");
        }

        /// <summary>
        /// 测试是否英文字母
        /// </summary>
        [TestMethod]
        public void TestIsLetter()
        {
            var str = "1";
            Assert.IsFalse(str.IsLetter(), "str.IsLetter()");
            str = "a";
            Assert.IsTrue(str.IsLetter(), "str.IsLetter()");
        }

        /// <summary>
        /// 测试是否大写英文字母
        /// </summary>
        [TestMethod]
        public void TestIsCapitalLetter()
        {
            var str = "a";
            Assert.IsFalse(str.IsCapitalLetter(), "str.IsCapitalLetter()");
            str = "A";
            Assert.IsTrue(str.IsCapitalLetter(), "str.IsCapitalLetter()");
        }

        /// <summary>
        /// 测试是否小写英文字母
        /// </summary>
        [TestMethod]
        public void TestIsLowerCaseLetter()
        {
            var str = "A";
            Assert.IsFalse(str.IsLowerCaseLetter(), "str.IsLowerCaseLetter()");
            str = "a";
            Assert.IsTrue(str.IsLowerCaseLetter(), "str.IsLowerCaseLetter()");
        }

        /// <summary>
        /// 测试是否数字和字母
        /// </summary>
        [TestMethod]
        public void TestIsNumAndLetter()
        {
            var str = "_";
            Assert.IsFalse(str.IsNumAndLetter(), "str.IsNumAndLetter()");
            str = "a1A";
            Assert.IsTrue(str.IsNumAndLetter(), "str.IsNumAndLetter()");
        }

        /// <summary>
        /// 测试是否数字、字母和下划线
        /// </summary>
        [TestMethod]
        public void TestIsNumAndLetterAndUnderline()
        {
            var str = "!";
            Assert.IsFalse(str.IsNumAndLetterAndUnderline(), "str.IsNumAndLetterAndUnderline()");
            str = "_a1A";
            Assert.IsTrue(str.IsNumAndLetterAndUnderline(), "str.IsNumAndLetterAndUnderline()");
        }

        /// <summary>
        /// 测试是否指定长度的用户名
        /// </summary>
        [TestMethod]
        public void TestIsUserNameWithSpecifiedLength()
        {
            var str = "_a1A";
            Assert.IsFalse(str.IsUserNameWithSpecifiedLength(), "str.IsUserNameWithSpecifiedLength()");
            str = "1a_A";
            Assert.IsFalse(str.IsUserNameWithSpecifiedLength(), "str.IsUserNameWithSpecifiedLength()");
            str = "a_1A";
            Assert.IsFalse(str.IsUserNameWithSpecifiedLength(), "str.IsUserNameWithSpecifiedLength()");
            Assert.IsTrue(str.IsUserNameWithSpecifiedLength(1, 4), "str.IsUserNameWithSpecifiedLength()");
            str = "a_1Aaa";
            Assert.IsTrue(str.IsUserNameWithSpecifiedLength(), "str.IsUserNameWithSpecifiedLength()");
        }

        /// <summary>
        /// 测试是否汉字
        /// </summary>
        [TestMethod]
        public void TestIsChineseCharacter()
        {
            var str = "zhongwen";
            Assert.IsFalse(str.IsChineseCharacter(), "str.IsChineseCharacter()");
            str = "中文";
            Assert.IsTrue(str.IsChineseCharacter(), "str.IsChineseCharacter()");
        }

        /// <summary>
        /// 测试是否身份证
        /// </summary>
        [TestMethod]
        public void TestIsIdNumStr()
        {
            var idNum = "123456789012345";
            Assert.IsFalse(idNum.IsIdNumStr(), "str.IsIdNumStr()");
            idNum = "123456789012345678";
            Assert.IsFalse(idNum.IsIdNumStr(), "str.IsIdNumStr()");
            idNum = "180149195705130002";
            Assert.IsTrue(idNum.IsIdNumStr(), "str.IsIdNumStr()");
            idNum = "130503670401001";
            Assert.IsTrue(idNum.IsIdNumStr(), "str.IsIdNumStr()");
        }

        /// <summary>
        /// 测试是否电子邮件
        /// </summary>
        [TestMethod]
        public void TestIsEmail()
        {
            var emailStr = "2ab@163.com";
            Assert.IsTrue(emailStr.IsEmail(), "str.IsEmail()");
            emailStr = "ab@dd";
            Assert.IsFalse(emailStr.IsEmail(), "str.IsEmail()");
            emailStr = "@dd";
            Assert.IsFalse(emailStr.IsEmail(), "str.IsEmail()");
        }

        /// <summary>
        /// 测试是否手机号码
        /// </summary>
        [TestMethod]
        public void TestIsPhone()
        {
            var phoneStr = "12345678901";
            Assert.IsFalse(phoneStr.IsPhone(), "str.IsPhone()");
            phoneStr = "13636993634";
            Assert.IsTrue(phoneStr.IsPhone(), "str.IsPhone()");
        }

        /// <summary>
        /// 测试是否电话号码
        /// </summary>
        [TestMethod]
        public void TestIsTel()
        {
            var telStr = "01087654321";
            Assert.IsTrue(telStr.IsTel(), "telStr.IsTel()");
            telStr = "010-87654321";
            Assert.IsTrue(telStr.IsTel(), "telStr.IsTel()");
            telStr = "0591-87654321";
            Assert.IsTrue(telStr.IsTel(), "telStr.IsTel()");
            telStr = "0591-7654321";
            Assert.IsTrue(telStr.IsTel(), "telStr.IsTel()");
            telStr = "05191-7654321";
            Assert.IsFalse(telStr.IsTel(), "telStr.IsTel()");
        }

        /// <summary>
        /// 测试是否邮政编码
        /// </summary>
        [TestMethod]
        public void TestIsPostalCode()
        {
            var postCode = "404000";
            Assert.IsTrue(postCode.IsPostalCode());
            postCode = "022001";
            Assert.IsFalse(postCode.IsPostalCode());
        }

        /// <summary>
        /// 测试是否qq
        /// </summary>
        [TestMethod]
        public void TestIsQq()
        {
            var qqStr = "10000";
            Assert.IsTrue(qqStr.IsQq());
            qqStr = "9999";
            Assert.IsFalse(qqStr.IsQq());
        }

        /// <summary>
        /// 测试是否ip地址
        /// </summary>
        [TestMethod]
        public void TestIsIpAddress()
        {
            var ipAddr = "192.168.1.1";
            Assert.IsTrue(ipAddr.IsIpAddress());
            ipAddr = "0.0.0.0";
            Assert.IsFalse(ipAddr.IsIpAddress());
        }

        #endregion

        #region 根据字符串长度处理字符串

        /// <summary>
        /// 测试获取字符串长度
        /// </summary>
        [TestMethod]
        public void TestGetLength()
        {
            var str = "中国";
            var firstLength = str.GetLength();
            str = "ab";
            var secondLength = str.GetLength();
            Assert.IsFalse(firstLength.Equals(secondLength),"firstLength.Equals(secondLength)");
        }


        /// <summary>
        /// 测试按指定的长度截取字符串
        /// </summary>
        [TestMethod]
        public void TestGetSubStrByLength()
        {
            var str = "hello,中国人";
            var length = str.GetLength();
            var fixStr = "......";
            var subStr = str.GetSubStrByLength(length, fixStr);
            if (length % 2 == 0)
            {
                Assert.AreEqual(str, subStr);
            }
            else
            {
                Assert.AreEqual(str + fixStr, subStr);
            }

            subStr = str.GetSubStrByLength(3, fixStr);
            Assert.AreEqual(string.Concat("he", fixStr), subStr);
            subStr = str.GetSubStrByLength(4, fixStr);
            Assert.AreEqual("hell", subStr);
            subStr = str.GetSubStrByLength(7, fixStr);
            Assert.AreEqual(string.Concat("hello,", fixStr), subStr);
            subStr = str.GetSubStrByLength(8, fixStr);
            Assert.AreEqual("hello,中", subStr);
        }

        #endregion
    }
}