using System;
using System.Text;
namespace ConsoleApp5

{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Введіть пароль для перевірки: ");
            string parol = Console.ReadLine();

            if (parolNadiyniy(parol))
            {
                Console.WriteLine("Пароль надійний");
            }
            else
            {
                Console.WriteLine("Пароль ненадійний");
            }

            
        }
        static bool parolNadiyniy(string parol)
        {
            if (parol.Length < 8)
            {
                return false;
            }

            bool velyki = false;
            bool malenki = false;
            bool cifry = false;

            foreach (char c in parol) {
                if (char.IsUpper(c))               
                    velyki = true;     
                
                else if (char.IsLower(c))               
                    malenki = true;  
                
                else if (char.IsDigit(c))
                    cifry = true;

                else if(c != '_')
                {
                    return false;
                }  
            }
            return velyki && malenki && cifry;
        }
    }
}
