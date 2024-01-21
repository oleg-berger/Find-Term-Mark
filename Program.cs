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

                               
            float finalMark = forUnit.SumAVGMark(FA, forTerm);

            Console.WriteLine(finalMark.ToString("F1"));
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

            if (userMarks.Length != maxMarks.Length)
                throw new ArgumentException("Ошибка: вы ввели разное количество оценок, попробуйте снова");
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

        public float SumAVGMark(Mark foMark, Mark forTerm)
        {
            int sumUserMarkForUnit = SumMark(userMark);

            int sumMaxMarkForUnit = SumMark(maxMark);

            int sumFOUsermark = SumMark(foMark.userMark);

            int sumMaxFOMark = SumMark(foMark.maxMark);

            int forTermUserSum = SumMark(forTerm.userMark);

            int forTermMAXSum = SumMark(forTerm.maxMark);

            //double AVGMark = (double)(sumUserMarkForUnit + sumFOUsermark) / (sumMaxMarkForUnit + sumMaxFOMark)) * 50 + (double)forTermUserSum / forTermMAXSum * 50;
            float AVGMark = ((float)(sumUserMarkForUnit + sumFOUsermark) / (sumMaxMarkForUnit + sumMaxFOMark)) * 50 + (float)forTermUserSum / forTermMAXSum * 50;


            return AVGMark;

        }

        public static int SumMark(int[] ranked)
        {
            int sumMarks = 0;
            for (int i = 0; i < ranked.Length; i++)
            {
                sumMarks += ranked[i];
            }

            return sumMarks;
        }

    }
}
