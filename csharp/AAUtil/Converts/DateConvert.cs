using System;
using System.Globalization;

namespace AAUtil.Converts
{
    /// <summary>
    /// 转换为日期
    /// </summary>
    public static class DateConvert
    {
        /// <summary>
        /// 尝试解析日期,可以解析的格式包括:
        /// yyyyMMdd,yyyy-MM-dd,yyyy/MM/dd,yyyy--MM-dd,yyyy/MM/dd,
        /// 原则上忽略前置空格和连接符(.,-/)(符号要一致)并在找到8位日期数字后按照yyyyMMdd的格式进行解析
        /// </summary>
        public static bool TryParseDate(string dateStr, out DateTime date)
        {
            date = DateTime.MinValue;

            if (string.IsNullOrEmpty(dateStr) || dateStr.Length < 8)
            {
                return false;
            }

            var index = 0;
            var chArr = new char[8];
            var splitChar = char.MinValue;

            foreach (var ch in dateStr)
            {
                if (char.IsWhiteSpace(ch))
                {
                    if (index < 1)
                    {
                        continue;
                    }

                    if (index == 8)
                    {
                        break;
                    }

                    return false;
                }

                if (ch < '0' || ch > '9')
                {
                    if (splitChar == char.MinValue)
                    {
                        if (ch == '.' || ch == ',' || ch == '-' || ch == '/')
                        {
                            splitChar = ch;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (ch != splitChar)
                    {
                        return false;
                    }
                }
                else
                {
                    chArr[index++] = ch;

                    if (index >= 8)
                    {
                        break;
                    }
                }
            }

            if (index < 8)
            {
                return false;
            }

            return DateTime.TryParseExact(new string(chArr), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        /// <summary>
        /// 整数解析成日期
        /// </summary>
        public static bool TryParseIntToDate(int number, out DateTime dt)
        {
            dt = DateTime.MinValue;

            var year = number / 10000;
            if (year < 1) return false;

            number %= 10000;

            var month = number / 100;
            if (month < 1 || month > 12) return false;

            var day = number % 100;
            if (day < 1 || day > 31) return false;

            dt = new DateTime(year, month, day);

            return true;
        }
    }
}
