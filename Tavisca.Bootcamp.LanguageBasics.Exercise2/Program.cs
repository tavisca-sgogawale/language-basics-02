
using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        
        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {         
            int hour = 0, minute = 0, second = 0;
            int lenPostTime = exactPostTime.Length;
            for (int i = 0; i < lenPostTime; i++)
            {
                for (int j = i + 1; j < lenPostTime; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (!string.Equals(showPostTime[i], showPostTime[j]))
                        {
                            return "impossible";
                        }
                    }
                }
            }
            string[] resultTime = new string[lenPostTime];
            for (int i = 0; i < lenPostTime; i++)
            {
                string hourString, minuteString, secondString;
                string[] exactTime = exactPostTime[i].Split(':');
                string[] showTime = showPostTime[i].Split(' ');
                hour = int.Parse(exactTime[0]);
                minute = int.Parse(exactTime[1]);
                second = int.Parse(exactTime[2]);
                secondString = second.ToString();
                
                if (showPostTime[i].Contains("minutes"))
                {
                    int mins = int.Parse(showTime[0]);
                    minute = minute + mins;
                    if (minute > 59)
                    {
                        minute = minute - 60;
                        if (minute == 0)
                        {
                            minuteString = "00";
                        }
                        else
                        {
                            minuteString = minute.ToString();
                        }

                        hour++;
                        if (hour > 23)
                        {
                            hour = hour - 24;
                            if (hour == 0)
                            {
                                hourString = "00";
                            }
                            else
                            {
                                hourString = hour.ToString();
                            }
                        }
                        else
                        {
                            hourString = hour.ToString();
                        }
                    }
                    else
                    {
                        minuteString = minute.ToString();
                        hourString = hour.ToString();
                    }
                }
                else if (showPostTime[i].Contains("hours"))
                {
                    minuteString = minute.ToString();
                    int hrs = int.Parse(showTime[0]);
                    hour = hour + hrs;
                    if (hour > 23)
                    {
                        hour = hour - 24;
                        if (hour == 0)
                        {
                            hourString = "00";
                        }
                        else
                        {
                            hourString = hour.ToString();
                        }
                    }
                    else
                    {
                        hourString = hour.ToString();
                    }
                }
                else
                {
                    hourString = hour.ToString();
                    minuteString = minute.ToString();
                }       
                resultTime[i] = hourString + ":" + minuteString + ":" + secondString;
            }
            Array.Sort(resultTime);
            Console.WriteLine(resultTime[lenPostTime - 1]);
            return resultTime[lenPostTime - 1];
        }
    }
}
