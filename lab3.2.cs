using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program2
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Введіть розмірність матриці: ");
            int romirnist = int.Parse(Console.ReadLine());

            int[,] matrica = new int[romirnist, romirnist];
            Console.WriteLine("Введіть елементи матриці: ");
            for (int rad = 0; rad < romirnist; rad++)
            {
                for (int stovp = 0; stovp < romirnist; stovp++)
                {
                    Console.Write($"Елемент [{rad + 1},{stovp + 1}]: ");
                    matrica[rad, stovp] = int.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine("Матриця дорівнює: ");
            for (int rad = 0; rad < romirnist; rad++)
            {
                for (int stovp = 0; stovp < romirnist; stovp++)
                {
                    Console.Write(matrica[rad, stovp]+" ");
                }
                Console.WriteLine();
            }
            int suma = 0;
            int dobutok = 1;

            for (int rad = 0; rad < romirnist; rad++)
            {
                for (int stovp = 0; stovp < romirnist; stovp++)
                {
                    suma += matrica[rad, stovp];
                    dobutok *= matrica[rad, stovp];
                }
            }
            int riznica = suma - dobutok;
            Console.WriteLine($"\nРізниця між сумою і добутком елементів матриці: {riznica}");

            for (int rad = 0; rad < romirnist; rad++)
            {
                int maksimalIndex = 0;
                for (int stovp = 0; stovp < romirnist; stovp++)
                {
                    if (matrica[rad, stovp] > matrica[rad, maksimalIndex])
                    {
                        maksimalIndex = stovp;
                    }
                }
                for (int stovp = maksimalIndex + 1; stovp < romirnist - 1; stovp++)
                {
                    for (int l = stovp + 1; l < romirnist; l++)
                    {
                        if (matrica[rad, stovp] > matrica[rad, l])
                        {
                            int elem = matrica[rad, stovp];
                            matrica[rad, stovp] = matrica[rad, l];
                            matrica[rad, l] = elem;
                        }
                    }
                }
            }
            Console.WriteLine("Оновлена матриця: ");
            for (int rad = 0; rad < romirnist; rad++)
            {
                for (int stovp = 0; stovp < romirnist; stovp++)
                {
                    Console.Write(matrica[rad, stovp] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

