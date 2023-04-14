using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfSunnyUILibrary.Class
{
    public class PublicClass
    {
        public static int GetHanNumFromString(string str)
        {
            int count = 0;
            Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");

            for (int i = 0; i < str.Length; i++)
            {
                if (regex.IsMatch(str[i].ToString()))
                {
                    count++;
                }
            }

            return count;
        }

        public static int GetTotalNumFromString(string str)
        {
            int length = str.Length;
            int hanlength = GetHanNumFromString(str);
            return length + hanlength;

        }
    }
}
