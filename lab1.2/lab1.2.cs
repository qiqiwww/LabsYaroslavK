using System;
using System.IO;

namespace lab1._2

{
    internal class Program
    {


        static void Main()
        {
            double y = 0, x = 0, maxx = 0, minx = 0;

            //in
            string[] lines = File.ReadAllLines(path: "input.txt");
            if (lines.Length > 0)
            {
                string[] values = lines[0].Split(' ');
                y = Convert.ToDouble(values[0]);
                x = Convert.ToDouble(values[1]);
                minx = Convert.ToDouble(values[2]);
                maxx = Convert.ToDouble(values[3]);
            }

                double krok = (maxx - minx) / 8;


                //out
                using (StreamWriter zapys = new StreamWriter(path: "output.txt"))
                {
                    zapys.WriteLine("Таблиця значень\n|--------------------------|");
                    zapys.WriteLine($"|{"X",5}{"|",6}{"Функція",11}{"|",5}");
                    zapys.WriteLine("|--------------------------|");
                  
                for (x= minx; x<=maxx;x+=krok)
                    {
                        y = Math.Sin((Math.PI * x) / 2) / (Math.Exp(x));
                        zapys.WriteLine($"|X={x,6:F2}{"|",3} Y={y,6:F4}{"|",7}");
                    }
                zapys.WriteLine("|--------------------------|");
                zapys.WriteLine("Таблицю Сформував К.Я");

            }


            }
        }
    }

