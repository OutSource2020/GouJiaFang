using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassHelpMe
    {
        // Number随机数个数
        // minNum随机数下限
        // maxNum随机数上限
        public int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }






        // Generate a random number between two numbers    
        //在兩個數字之間生成一個隨機數
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            return random.Next(min, max);
        }

        // Generate a random string with a given size  
        //生成具有給定大小的隨機字符串
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        //生成隨機密碼
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }






        /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length,int a1,int a2)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(a1,a2));
            }
            return result.ToString();
        }



    }
}
