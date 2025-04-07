using System;

namespace ConsoleApp6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть рядок:");
            string vvid = Console.ReadLine(); 

            string pravilRadok = "abcdefghijklmnopqrstuv18340"; 

           
            if (vvid == pravilRadok)
            {
                Console.WriteLine("Рядок правильний.");
            }
            else
            {
                Console.WriteLine("Рядок неправильний.");
            }
        }
    }
}

