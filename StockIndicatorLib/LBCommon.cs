
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StockIndicatorLib
{
    public class LbCommon
    {
        private static int _SerialNumber = 0;

        public static void getDateByNum(double num, ref int tm_year, ref int tm_mon, ref int tm_mday, ref int tm_hour, ref int tm_min, ref int tm_sec, ref int tm_msec)
        {
            DateTime time2 = new DateTime(0x7b2, 1, 1);
            DateTime time = new DateTime(time2.Ticks + (((long)num) * 0x989680L));
            tm_year = time.Year;
            tm_mon = time.Month;
            tm_mday = time.Day;
            tm_hour = time.Hour;
            tm_min = time.Minute;
            tm_sec = time.Second;
        }

        public static double getDateNum(int tm_year, int tm_mon, int tm_mday, int tm_hour, int tm_min, int tm_sec, int tm_msec)
        {
            TimeSpan span = (TimeSpan)(new DateTime(tm_year, tm_mon, tm_mday, tm_hour, tm_min, tm_sec) - new DateTime(0x7b2, 1, 1));
            return span.TotalSeconds;
        }



        public static int GetFibonacciValue(int index)
        {
            if (index < 1)
            {
                return 0;
            }
            List<int> list = new List<int>();
            for (int i = 0; i <= (index - 1); i++)
            {
                if ((i == 0) || (i == 1))
                {
                    list.Add(1);
                }
                else
                {
                    list.Add(list[i - 1] + list[i - 2]);
                }
            }
            return list[index - 1];
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static double GetHighValue(List<double> valueList)
        {
            double num = 0.0;
            for (int i = 0; i < valueList.Count; i++)
            {
                if (i == 0)
                {
                    num = valueList[i];
                }
                else if (num < valueList[i])
                {
                    num = valueList[i];
                }
            }
            return num;
        }

        public static double[] GetLineParam(float x1, float y1, float x2, float y2)
        {
            double num = 0.0;
            if (!((x2 - x1) == 0f))
            {
                num = (y2 - y1) / (x2 - x1);
                double num2 = y1 - (num * x1);
                return new double[] { num, num2 };
            }
            return null;
        }

        public static double GetLowValue(List<double> valueList)
        {
            double num = 0.0;
            for (int i = 0; i < valueList.Count; i++)
            {
                if (i == 0)
                {
                    num = valueList[i];
                }
                else if (num > valueList[i])
                {
                    num = valueList[i];
                }
            }
            return num;
        }

        public static Color GetReverseColor(Color originalColor)
        {
            return Color.FromArgb(0xff - originalColor.R, 0xff - originalColor.G, 0xff - originalColor.B);
        }


        public static string GetValueByDigit(double value, int digit, bool round)
        {
            if (digit == 2)
            {
                bool fda = true;
            }
            int num;
            if (!round)
            {
                StringBuilder builder = new StringBuilder();
                string str = value.ToString();
                if (str.IndexOf(".") != -1)
                {
                    builder.Append(str.Substring(0, str.IndexOf(".")));
                    if (digit > 0)
                    {
                        builder.Append(".");
                    }
                    for (num = 0; num < digit; num++)
                    {
                        int startIndex = str.IndexOf(".") + (num + 1);
                        if (startIndex <= (str.Length - 1))
                        {
                            builder.Append(str.Substring(startIndex, 1));
                        }
                        else
                        {
                            builder.Append("0");
                        }
                    }
                }
                else
                {
                    builder.Append(str);
                    if (digit > 0)
                    {
                        builder.Append(".");
                    }
                    for (num = 0; num < digit; num++)
                    {
                        builder.Append("0");
                    }
                }
                return builder.ToString();
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("0");
            if (digit > 0)
            {
                builder2.Append(".");
                for (num = 0; num < digit; num++)
                {
                    builder2.Append("0");
                }
            }
            return value.ToString(builder2.ToString());
        }

        public static int GridScale(double min, double max, int yLen, int maxSpan, int minSpan, int defCount, ref double step, ref int digit)
        {
            double num = max - min;
            int num2 = (int)Math.Ceiling((double)(((double)yLen) / ((double)maxSpan)));
            int num3 = (int)Math.Floor((double)(((double)yLen) / ((double)minSpan)));
            int num4 = defCount;
            double num5 = num / ((double)num4);
            bool flag = false;
            double num6 = 0.0;
            int num7 = 0;
            int num8 = 0;
            step = 0.0;
            digit = 0;
            num4 = Math.Max(num2, num4);
            num4 = Math.Max(Math.Min(num3, num4), 1);
            for (num7 = 15; num7 >= -6; num7--)
            {
                num6 = Math.Pow(10.0, (double)num7);
                if (num6 < 1.0)
                {
                    digit++;
                }
                num8 = (int)Math.Floor((double)(num5 / num6));
                if (flag)
                {
                    if (num8 < 4)
                    {
                        if (digit > 0)
                        {
                            digit--;
                        }
                    }
                    else if ((num8 >= 4) && (num8 <= 6))
                    {
                        num8 = 5;
                        step += 5f * num6;
                    }
                    else
                    {
                        step += 10.0 * num6;
                        if (digit > 0)
                        {
                            digit--;
                        }
                    }
                    break;
                }
                if (num8 > 0)
                {
                    step = (num8 * num6) + step;
                    num5 -= step;
                    flag = true;
                }
            }
            return 0;
        }

        public static double[] LinearRegressionEquation(List<double> vList)
        {
            if (vList.Count > 1)
            {
                int num3;
                double num = 0.0;
                double num2 = 0.0;
                for (num3 = 0; num3 < vList.Count; num3++)
                {
                    num += num3 + 1;
                    num2 += vList[num3];
                }
                double num4 = num / ((double)vList.Count);
                double num5 = num2 / ((double)vList.Count);
                double num6 = 0.0;
                double num7 = 0.0;
                for (num3 = 0; num3 < vList.Count; num3++)
                {
                    num6 += ((num3 + 1) - num4) * (vList[num3] - num5);
                    num7 += ((num3 + 1) - num4) * ((num3 + 1) - num4);
                }
                double num8 = num6 / num7;
                double num9 = num5 - (num8 * num4);
                return new double[] { num8, num9 };
            }
            return null;
        }

        public static int SerialNumber
        {
            get
            {
                return ++_SerialNumber;
            }
        }
    }
}