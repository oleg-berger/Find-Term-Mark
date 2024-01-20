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
        }
        class Mark
        {
            public int userMark { get; private set; }
            public int maxMark { get; private set; }

            public Mark (int userMarks, int maxMarks)
            {
                userMark = userMarks;
                maxMark = maxMarks;
            }
    }
}
