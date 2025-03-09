namespace lab2._2
{
    internal class Program
    {

        static double Factorial(int n)
        {
            double rez = 1;
            for (int i = 1; i <= n; i++)
            {
                rez *= i;
            }
            return rez;
        }
        static double Obchislennya(double x, double meja = 1e-6) {
            double sumaryada = 0;
            double chlenryada=0;
            int nomerchlena = 1;

            while (Math.Abs(chlenryada) <= meja)  
            {  
                chlenryada = Math.Sin(Math.PI * nomerchlena / 4) * Math.Sqrt(Math.Pow(2, nomerchlena)) / Factorial(nomerchlena) * Math.Pow(x, nomerchlena);
               
                sumaryada += chlenryada;
                nomerchlena++;
            }
            return sumaryada;

        }
        static void Main()
        {
            double[] znachennyaX = { Math.PI / 4, Math.PI, Math.PI * -5.5 };

            foreach (double x in znachennyaX) {
                double sumaryada = Obchislennya(x);
                double y = Math.Exp(x)*Math.Sin(x);

                Console.WriteLine($"x = {x:F4},Сума рядка: {sumaryada:F6}, Точне значення: {y:F6}");
            }
        }
    }
}
