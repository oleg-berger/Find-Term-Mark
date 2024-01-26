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

            bool isOpen = true;

            while (isOpen)
            {

                Console.Clear();
                Console.SetCursorPosition(0, 25);

                Console.WriteLine("Добро пожаловать в программу для расчета оценки за четверть");
                Console.WriteLine("1 - расчет оценок   2 - мои оценки  3 - выход");

                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.SetCursorPosition(0, 0);

                            DataBase dataBase = new DataBase();
                            string userName;

                            Console.Write("Введите ваше имя ");

                            userName = Console.ReadLine();

                            Console.Write("Введите оценку за СОР ");
                            int[] userForUnit = EnterMark();
                            
                            Console.Write("Введите оценку за СОЧ ");
                            int[] userForTerm = EnterMark();
                            Console.Write("Введите оценку за ФО ");
                            int[] userFA = EnterMark(); // FA -- its mean Formative Assessment

                            int[] maxFA = new int[userFA.Length];
                            for (int i = 0; i < userFA.Length; i++)
                            {
                                maxFA[i] = 10;
                            }

                            Mark FA = new Mark(userFA, maxFA);
                            FA.CheckRightNum();

                            
                            Console.Write("Введите максимальный балл за СОР ");
                            int[] maxMarkForUnit = EnterMark();

                            Mark forUnit = new Mark(userForUnit, maxMarkForUnit);
                            forUnit.CheckRightNum();



                            Console.Write("Введите максимальный балл за СОЧ ");
                            int[] maxMarkForTerm = EnterMark();

                            Mark forTerm = new Mark(userForTerm, maxMarkForTerm);
                            forTerm.CheckRightNum();






                            float finalMark = forUnit.SumAVGMark(FA, forTerm);

                            Console.WriteLine(finalMark.ToString("F1"));

                            int totalMark = (int)finalMark;


                            forTerm.InsertMarkIntoDatabase(dataBase, userName, userForUnit, maxMarkForUnit, userFA, userForTerm, maxMarkForTerm, (int)finalMark);

                            int userId = dataBase.GetUserId(userName);

                            if (userId != -1)
                                Console.WriteLine("Добавлен новый пользователь с id: " + userId);
                            else
                                Console.WriteLine("Не удалось получить id нового пользователя.");



                            Console.ReadKey();

                            break;

                        case 2:

                            break;

                        case 3:

                            break;

                        default:
                            Console.WriteLine("Ошибка: Введено не верное значение");
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Введено недопустимое значение");
                    break;
                }




            }
        }


        static int[] EnterMark()
        {
            string strArray = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(strArray))
            {
                Console.WriteLine("Ошибка: введена пустая строка");
                Environment.Exit(0);
            }


            char[] separators = { ' ', ',', '|', ':', ';', '.' };

            string[] strArray2 = strArray.Split(separators, StringSplitOptions.RemoveEmptyEntries);



            int[] marks = new int[strArray2.Length];


            try
            {
                for (int i = 0; i < marks.Length; i++)
                {
                    marks[i] = Convert.ToInt32(strArray2[i]);
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введена строка а не число пожалуйста повторите попытку");
                Environment.Exit(0);
            }

            for (int k = 0; k < marks.Length; k++)
            {
                if (marks[k] > 40 || marks[k] < 0)
                {
                    Console.WriteLine("Ошибка: Введено недопустимое значение, пожалуйста введите числа в диапозоне от 0 до 40");
                    Environment.Exit(0);
                }
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


        

        public void InsertMarkIntoDatabase(DataBase dataBase, string userName, int[] forUnit, int[] maxForUnit, int[] forFa, int[] forTerm, int[] maxForTerm, int totalMark)
        {
            string strUserForUnit = StringToInt(forUnit);

            string maxStrForUnit = StringToInt(maxForUnit);

            string strUserForFA = StringToInt(forFa);

            string strUserForTerm = StringToInt(forTerm);

            string maxStrForTerm = StringToInt(maxForTerm);

            dataBase.InsertMark(userName, strUserForUnit, maxStrForUnit, strUserForFA, strUserForTerm, maxStrForTerm, totalMark);

        }



        static public string StringToInt(int[] nums)
        {
            StringBuilder strNum = new StringBuilder();
            for (int i = 0; i < nums.Length; i++)
            {
                strNum.Append(nums[i].ToString());
                if (i < nums.Length - 1)
                    strNum.Append(' ');
            }

            return strNum.ToString();
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

            if (sumMaxMarkForUnit + sumMaxFOMark == 0)
            {
                Console.WriteLine("Ошибка: Сумма максимальных баллов равна нулю. Невозможно выполнить деление.");
                Environment.Exit(0);
            }

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

        public void CheckRightNum()
        {

            if (userMark.Length != maxMark.Length)
            {
                Console.WriteLine("Ошибка: вы ввели разное количество оценок, попробуйте снова");
                Environment.Exit(0);
            }


            for (int i = 0; i < userMark.Length; i++)
            {
                if (userMark[i] > maxMark[i])
                {
                    Console.WriteLine("Ошибка: ваша оценка выше максимальной");
                    Environment.Exit(0);
                }

            }

        }


    }
}
