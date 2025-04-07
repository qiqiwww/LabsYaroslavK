using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Виберіть спосіб введення даних:");
            Console.WriteLine("1 - З файлу");
            Console.WriteLine("2 - З клавіатури");
            

            int vibir = int.Parse(Console.ReadLine());
            int[]chisla= null;

            switch (vibir) {
                case 1:
                    chisla = zfauly();
                    break;
                case 2:
                    chisla = zklaviaturi();
                    break;
                
                
            }
            rozrahunky(chisla);
        }


        static int[] zfauly()
        {
            
            
            string[] lines = File.ReadAllText(path:"input.txt").Split(' ');

            int[] chisla = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                chisla[i] = int.Parse(lines[i]);
            }
            return chisla;
        }


        static int[] zklaviaturi()
        {
            Console.Write("Введіть кількість чисел: ");
            int kilkist = int.Parse(Console.ReadLine());  

            int[] numbers = new int[kilkist];
            Console.WriteLine("Введіть числа:");

            for (int i = 0; i < kilkist; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());  
            }
            return numbers;
        }


        static void rozrahunky(int[] mas)
        {
            int minIndex = 0, maxIndex = 0;

            
            for (int i = 1; i < mas.Length; i++)
            {
                if (mas[i] < mas[minIndex]) 
                    minIndex = i;  
                if (mas[i] > mas[maxIndex]) 
                    maxIndex = i; 
            }

            
            Console.WriteLine($"Мінімальний елемент дорівнює: {mas[minIndex]}");
            Console.WriteLine($"Максимальний елемент дорівнює: {mas[maxIndex]}");

            
            
            if (minIndex < maxIndex)
            {
                for (int i = minIndex + 1; i < maxIndex; i++)
                {
                    mas[i] *= 2;  
                }
            }
            else { for(int i = maxIndex + 1;i< minIndex; i++)
                {
                    mas[i] *= 2;
                } 
            }

            
            Console.WriteLine("Масив після обробки:");
            foreach (int n in mas)
            {
                Console.WriteLine(n);
            }
        }
    }

}
