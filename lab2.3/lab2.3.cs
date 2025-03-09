using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace lab2._3
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int ochki = 0;

            if (DlaRivnya(1, 10, 3, 50))
            {
                ochki += 50 * 3;
                Console.WriteLine($"Очки першого рівня {ochki}");

                if (DlaRivnya(10, 100, 2, 25))
                {
                    ochki += 25 * 10;
                    Console.WriteLine($"Очки другого рівня {ochki}");
                }
                
            }

            static bool DlaRivnya(int min, int max, int rayndy, int vidsotokdovjina)
            {
                int jittyi = (max - min + 1) * vidsotokdovjina / 100;
                Console.WriteLine($"Початок рівня. Наразі маєте {jittyi} життів.");

                for (int raynd = 1; raynd <= rayndy; raynd++)
                {
                    Console.WriteLine($"\nРаунд {raynd}");
                    if (!DlaRaundy(min, max, jittyi))
                        return false;
                }
                return true;

            }
            static bool DlaRaundy(int min, int max, int jittyi)
            {
                int zahadanechyslo = new Random().Next(min, max + 1);
                int vidpovid;

                while (jittyi > 0)
                {
                    Console.WriteLine("Введіть число: ");
                    vidpovid = int.Parse(Console.ReadLine());

                    if (vidpovid == zahadanechyslo)
                    {
                        Console.WriteLine("Ви вгадали");
                        return true;
                    }
                    if (vidpovid < zahadanechyslo)
                    {
                        Console.WriteLine("Загадане число більше");
                        jittyi--;
                    }
                    else
                    {
                        Console.WriteLine("Загадане число менше");
                        jittyi--;
                    }
                }
                Console.WriteLine("Ви програли");
                return false;
            }
            static int DlaRivnya2()
            {
                int jittyi = 25;
                int ochki = 0;

                for (int raynd = 1; raynd <= 2; raynd++)
                {
                    Console.WriteLine($"\n Раунд{raynd}");

                    if (!DlaRaundy(10, 100, jittyi))
                        return 0;

                    ochki += jittyi * 10;
                    Console.WriteLine($"Очок за раунд {raynd}:{jittyi * 10}");
                }
                return ochki;
            }
        }
    }
}
