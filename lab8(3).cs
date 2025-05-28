using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Func<int[], int> kratni7 = mas => mas.Count(chyslo => chyslo % 7 == 0);//lambda dlya pidrahunku chysel, shcho cratni 7

        
        Func<int[], int> dodatniChysla = mas => mas.Count(chyslo => chyslo > 0);//lambda dlya pidrahunku dodatnih chysel

        
        Predicate<DateTime> denProgramista = data =>//lambda dlya perevirky dnya prohramista
        {
            int rik = data.Year;
            DateTime den256 = new DateTime(rik, 1, 1).AddDays(255); 
            return data.Date == den256.Date;
        };

        
        Func<string, string[], bool> chyMistytSlova = (text, slova) =>//lambda dlya perevirky nayavnosti zadanoho slova abo masyvu sliv 
            slova.Any(slovo => text.IndexOf(slovo, StringComparison.OrdinalIgnoreCase) >= 0);

        //testuvannya
        int[] testoviChysla = { -7, 0, 7, 14, 21, -3, 42 };
        Console.WriteLine($"Кількість чисел кратних 7: {kratni7(testoviChysla)}"); 
        Console.WriteLine($"Кількість додатніх чисел: {dodatniChysla(testoviChysla)}"); 

        DateTime data = new DateTime(2023, 9, 13); 
        Console.WriteLine($"13.09.2023 — день програміста? {denProgramista(data)}");
        
        string text = "Буває, що й корова літає";
        string[] shukaniSlova = { "корова", "коли", "Буває" };
        Console.WriteLine($"Текст містить хоча б одне зі слів? {chyMistytSlova(text, shukaniSlova)}");
    }
}
