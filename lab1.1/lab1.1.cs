namespace lab1._1
{
    internal class Program
    {
        static void Main()
        {
            double x = 182.5;
            double y = 18.225;
            double z = -0.03298;

            double left = Math.Abs(Math.Pow(x, y / x) - (Math.Pow(y/x, 1/3)));
            double right = ( (y-x) *Math.Cos(y)-z)/(1+Math.Pow(y,2)-2*x*y+Math.Pow(x,2));
            double rez = left + right;

            Console.WriteLine(rez);
            Console.ReadKey();
        }
    }
}
