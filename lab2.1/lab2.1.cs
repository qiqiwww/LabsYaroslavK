namespace lab2._1
{
    internal class Program
    {
        static void Main()
        {
            double x = 0,a=0,y=0, krok =0.1, minx=-2,maxx=2;

            Console.WriteLine("x \t a \t y");

            for (x= minx; x <= maxx; x += krok)
            {
                for (a = 1.25; a <= 2; a += 0.25)
                {
                    y = (1 - Math.Exp(-Math.Pow(x / a, 2))) / a;
                    Console.WriteLine($"{x:F2} \t {a:F2} \t {y:F4}");
                }
                Console.WriteLine();
            }
        }
    }
}
