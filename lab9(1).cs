using System;
using System.IO;
using System.Linq;
using System.Text;

class FileStatsAnalyzer
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Уведіть шлях до файлу: ");
        string shlyah = Console.ReadLine();

        try
        {
            string text = File.ReadAllText(shlyah, Encoding.UTF8);

            
            int kilkistrechen = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;//osnovni dani
            int kilkistvelykyhliter = 0;
            int kilkistmalenkyhliter = 0;
            int kilkistholosnyh = 0;
            int kilkistpryholosnyh = 0;
            int kilkistcyfr = 0;

            
            char[] holosni = { 'а', 'є', 'е', 'и', 'і', 'ї', 'о', 'у', 'ю', 'я',//symvoly
                              'А', 'Є', 'Е', 'И', 'І', 'Ї', 'О', 'У', 'Ю', 'Я' };
            char[] pryholosni = "бвгґджзклмнпрстфхцчшщ.".ToCharArray();

            foreach (char c in text)
            {
                if (char.IsUpper(c)) kilkistvelykyhliter++;
                if (char.IsLower(c)) kilkistmalenkyhliter++;
                if (holosni.Contains(c)) kilkistholosnyh++;
                if (pryholosni.Contains(c)) kilkistpryholosnyh++;
                if (char.IsDigit(c)) kilkistcyfr++;
            }

            
            Console.WriteLine($"Кількість речень: {kilkistrechen}");//vyvid
            Console.WriteLine($"Кількість великих літер: {kilkistvelykyhliter}");
            Console.WriteLine($"Кількість маленьких літер: {kilkistmalenkyhliter}");
            Console.WriteLine($"Кількість голосних: {kilkistholosnyh}");
            Console.WriteLine($"Кількість приголосних: {kilkistpryholosnyh}");
            Console.WriteLine($"Кількість цифр: {kilkistcyfr}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не знайдено");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
