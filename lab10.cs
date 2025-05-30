namespace lab10
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    class Platizh
    {
        public string RahunokPlantyka { get; set; }    
        public string RahunokOtrymuvacha { get; set; } 
        public DateTime DataOplaty { get; set; }   
        public decimal Suma { get; set; }         

        public override string ToString() =>
            $"Дата: {DataOplaty:dd.MM.yyyy}  Платник: {RahunokPlantyka}  Отримувач: {RahunokOtrymuvacha}  Сума: {Suma:C}";
    }

    class Programa
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var platezhi = new List<Platizh>
        {
            new Platizh { RahunokPlantyka = "123456", RahunokOtrymuvacha = "654321", DataOplaty = new DateTime(2023, 05, 10), Suma = 1500.75m },
            new Platizh { RahunokPlantyka = "111222", RahunokOtrymuvacha = "333444", DataOplaty = new DateTime(2023, 05, 15), Suma = 800.00m },
            new Platizh { RahunokPlantyka = "555666", RahunokOtrymuvacha = "777888", DataOplaty = new DateTime(2023, 06, 01), Suma = 2500.50m },
            new Platizh { RahunokPlantyka = "999000", RahunokOtrymuvacha = "111000", DataOplaty = new DateTime(2023, 06, 20), Suma = 950.25m }
        };

            Console.Write("Початкова дата періоду (дд.мм.рррр): ");
            DateTime pochatkovaData = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Console.Write("Кінцева дата періоду (дд.мм.рррр): ");
            DateTime kincevaData = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Console.Write("Мінімальна сума платежу: ");
            decimal minimalniyPlatizh = decimal.Parse(Console.ReadLine());

            var filteredPayments = platezhi
                .Where(p => p.DataOplaty >= pochatkovaData && p.DataOplaty <= kincevaData && p.Suma >= minimalniyPlatizh);

            Console.WriteLine("\nЗнайдені платежі:");
            foreach (var payment in filteredPayments)
            {
                Console.WriteLine(payment);
            }
        }
    }
}
