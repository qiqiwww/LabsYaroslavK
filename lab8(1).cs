using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        //vidobrazhennya potochnoho chasu, daty, dnya tyzhnya
        Action showTime = () => Console.WriteLine($"Поточний час: {DateTime.Now.ToString("HH:mm:ss")}");
        Action showDate = () => Console.WriteLine($"Поточна дата: {DateTime.Now.ToShortDateString()}");
        Action showDayOfWeek = () => Console.WriteLine($"Поточний день тижня: {DateTime.Now.DayOfWeek}");

        
        Predicate<int> isPrime = chyslo =>//perevirka na proste chyslo
        {
            if (chyslo <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(chyslo); i++)
                if (chyslo % i == 0) return false;
            return true;
        };

        Predicate<int> isFibonacci = chyslo =>//perevirka na chyslo fibonachi
        {
            int a = 0, b = 1;
            while (b < chyslo)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return b == chyslo || chyslo == 0;
        };

        
        Func<double, double, double> ploshchatrykut = (baseLength, height) => (baseLength * height) / 2;//pidrahunok ploshchi trykutnyka
        Func<double, double, double> ploshchapryamokyt = (length, width) => length * width;//pidrahunok ploshchi pramokutnyka

        
        showTime();       //vyvid chasu
        showDate();       //daty
        showDayOfWeek();  //dnya

        Console.WriteLine($"Чи є 7 простим числом? {isPrime(7)}");           
        Console.WriteLine($"Чи є 8 простим числом? {isPrime(8)}");           
        Console.WriteLine($"Чи 5 числом Фібоначчі? {isFibonacci(5)}");     
        Console.WriteLine($"Чи є 4 числом Фібоначчі? {isFibonacci(4)}");    

        Console.WriteLine($"Площа трикутника (6, 4): {ploshchatrykut(6, 4)}");      
        Console.WriteLine($"Площа прямокутника (5, 3): {ploshchapryamokyt(5, 3)}");  
    }
}
