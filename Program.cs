using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите оценку за СОР");
            int[] userForUnit = EnterMark();
        }
         
        static int[] EnterMark()
        {
            string strArray = Console.ReadLine();
            string[] strArray2 = strArray.Split(' ');
            int[] marks = new int[strArray2.Length];

            for(int i = 0; i < marks.Length; i++)
            {
                marks[i] = Convert.ToInt32(strArray2[i]);
            }

            return marks;
        }

        }
    }
        class Mark
        {
            public int[] userMark { get; private set; }
            public int[] maxMark { get; private set; }

            public Mark(int[] userMarks, int[] maxMarks)
            {
                userMark = userMarks;
                maxMark = maxMarks;
            }
}
