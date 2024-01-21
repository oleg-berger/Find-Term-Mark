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
            Console.Write("Введите оценку за СОР ");
            int[] userForUnit = EnterMark();
            Console.Write("Введите оценку за СОЧ ");
            int[] userForTerm = EnterMark();
            Console.Write("Введите оценку за ФО ");
            int[] userFA = EnterMark(); // FA -- its mean Formative Assessment


            Console.Write("Введите максимальный балл за СОР ");
            int[] maxMarkForUnit = EnterMark();
            Console.Write("Введите максимальный балл за СОЧ ");
            int[] maxMarkForTerm = EnterMark();

            int[] maxFA = new int[userFA.Length];
            for (int i = 0; i < userFA.Length; i++)
            {
                maxFA[i] = 10;
            }



            Mark forUnit = new Mark(userForUnit, maxMarkForUnit);
            Mark forTerm = new Mark(userForTerm, maxMarkForTerm);
            Mark FA = new Mark(userFA, maxFA);

            

        }


        static int[] EnterMark()
        {
            string strArray = Console.ReadLine();
            string[] strArray2 = strArray.Split(' ');
            int[] marks = new int[strArray2.Length];

            for (int i = 0; i < marks.Length; i++)
            {
                marks[i] = Convert.ToInt32(strArray2[i]);
            }

            return marks;

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

        public void ShowMark()
        {
            foreach (int mark in userMark)
                Console.Write(mark + " ");

            Console.WriteLine("\n");

            foreach (int mark in maxMark)
                Console.Write(mark + " ");

            Console.WriteLine("\n");
        }

        /*  public static void SumMark(Mark forUnit, Mark forTerm, Mark FA)
          {
              int termMark = 
          }*/

    }
}
