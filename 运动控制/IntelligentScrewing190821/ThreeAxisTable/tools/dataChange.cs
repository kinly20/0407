using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
16进制到10进制?
十进制转二进制
Convert.ToString(69, 2); //69为被转值
十进制转八进制
Convert.ToString(69, 8); //69为被转值
十进制转十六进制
Convert.ToString(69, 16); //69为被转值
二进制转十进制
Convert.ToInt32(”100111101″, 2); //100111101为被转值
八进制转十进制
Convert.ToInt32(”76″, 8); //76为被转值
C# 16进制转换10进制
Convert.ToInt32(”FF”, 16); //FF为被转值
 */
namespace ThreeAxisTable.tools
{
    class dataChange
    {
        #region//数据转换
        public Int32 DInt16toI32(Int16 NL, Int16 NH)//合并两个I16
        {
            Int32 number;
            string HexL = String.Format("{0:X}", NL);//将数字转成16进制字符
            HexL = HexL.PadLeft(4, '0');//保证4位字符
            string HexH = String.Format("{0:X}", NH);
            HexH = HexH.PadLeft(4, '0');
            number = Convert.ToInt32(HexH + HexL, 16);
            return number;
        }
        public static void boolsToBytes(bool[] bools,ref byte[] bytes)
        {
            if (bools == null)
                return;
            
            for(int i=0;i<bools.Length;i++)
            {
                if(bools[i])
                {
                    int m=i/8;
                    int n = i % 8;
                    bytes[m] += (byte)Math.Pow(2,n);
                }
            }
        }
        public static void bytesToHixString(byte[] bytes,ref string[] strs)
        {
            if (bytes == null)
                return;

            for (int i = 0; i < bytes.Length; i++)
            {
                strs[i] = Convert.ToString(bytes[i], 16);
            }
        }
        public static short ConvertBoolArrayToShort(bool[] barray)
        {
            short result = 0;
            if (barray != null)
            {
                int len = barray.Length;

                for (int i = 0; i < len; i++)
                {
                    result += (short)((barray[i] ? 1 : 0) << i);
                }

            }
            else
            {
                //Console.WriteLine("bool数组为空。");

            }

            return result;
        }
        public static bool[] ConvertIntToBoolArray(int result, int len)
        {

            if (len > 32 || len < 0)
            {
                //Console.WriteLine("bool数组长度应该在0到32之间。");
            }

            bool[] barray2 = new bool[len];

            for (int i = 0; i < len; i++)
            {
                barray2[i] = (((uint)result >> i) % 2) == 1;
            }

            return barray2;

        }
        public static int HexToInt(string strHex)
        {
            int intHex = 0;
            intHex = BitHexToInt(strHex[3]) * 256 + BitHexToInt(strHex[2]) * 4096 + BitHexToInt(strHex[1]) + BitHexToInt(strHex[0]) * 16;
            return intHex;
        }
        public static string IntToHex(int data)
        {
            return Convert.ToString(data, 16);

        }
        private static int BitHexToInt(char chrHex)
        {
            if (chrHex >= '0' && chrHex <= '9')
                return chrHex - '0';
            if (chrHex >= 'A' && chrHex <= 'F')
                return chrHex - 'A' + 10;
            if (chrHex >= 'a' && chrHex <= 'f')
                return chrHex - 'a' + 10;
            return 0;
        }
        #endregion

        #region//数据测试

        #endregion
        #region//取位值
        
        public static bool ShortGetBit(uint indata,int index)
        {
            int n = 15 - index;
            ushort temp=(ushort)(indata << n);
            temp =(ushort)(temp >> n+index);
            if (temp == 1)
                return true;
            return false;
        }
        #endregion
     
    }
}
