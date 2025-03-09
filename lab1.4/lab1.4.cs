using System;
using System.IO;

namespace lab1._4
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int emnistbaka = 0,vidstanAB=0, vidstanBC=0, vaga=0;
            

            string[] lines = File.ReadAllLines(path: "input.txt");
            if (lines.Length > 0)
            {
                string[]values = lines[0].Split(' ');

                emnistbaka=Convert.ToInt32(values[0]);
                vidstanAB = Convert.ToInt32(values[1]);
                vidstanBC = Convert.ToInt32(values[2]);
                vaga = Convert.ToInt32(values[3]);

                
            }

            int palivonakilometr = Obchislennya(vaga);

            Perevirkapaliva(palivonakilometr, vidstanAB, vidstanBC, emnistbaka);

          
        }

        static public void Perevirkapaliva(int palivonakilometr, int vidstanAB, int vidstanBC,int emnistbaka)
        {
            double zahalnyakilkistpaliva = (vidstanAB + vidstanBC) * palivonakilometr;
            

            if (palivonakilometr == -1)
            {
                Console.WriteLine("Літак не в змозі піднятися з такою вагою вантажа");
            }

            if (zahalnyakilkistpaliva <= emnistbaka)
            {
                Console.WriteLine("Палива достатньо для виконанн рейсу");
            }
            else
            {
                double kilkistpalivadlaAB = vidstanAB * palivonakilometr;
                double zalyshokpalivapislaAB = emnistbaka - kilkistpalivadlaAB;
                if (zalyshokpalivapislaAB < 0)
                {
                    Console.WriteLine("Палива не вистачить для польоту з точки А у точку Б");
                }
                else
                {
                    double kilkistpalivadlaBC = vidstanBC * palivonakilometr;
                    double kilkistneobhidnohopaliva = kilkistpalivadlaBC - zalyshokpalivapislaAB;
                    if (kilkistpalivadlaBC <= 0)
                    {
                        Console.WriteLine("Палива достатньо для виконання рейсу");
                    }
                    else
                    {
                        Console.WriteLine($"Потрібно дозаправити {kilkistneobhidnohopaliva} літрів в точці B");
                    }
                }
            }
        }




        static public int Obchislennya(int vaga) {
            int[] spojivannyapalyva = { 500, 1000, 1500, 2000 };
            int[] litry = { 1, 4, 7, 9 };
            for (int i = 0; i < spojivannyapalyva.Length; i++) {
                if (vaga <= spojivannyapalyva[i])
                {
                    return litry[i];
                }
            
            }
            return -1;
        }
    }
}
