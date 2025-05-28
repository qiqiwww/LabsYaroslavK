using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Censor
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Уведіть шлях до файлу з текстом:");
        string shlyahDotextu = Console.ReadLine();

        Console.WriteLine("Уведіть шлях до файлу з забороненими словами:");
        string shlahDosliv = Console.ReadLine();

        try
        {
            // Читання вхідних файлів
            string text = File.ReadAllText(shlyahDotextu, Encoding.UTF8);
            string[] zaboronenislova = File.ReadAllLines(shlahDosliv, Encoding.UTF8);

            // Обробка кожного забороненого слова
            foreach (string slovo in zaboronenislova)
            {
                if (!string.IsNullOrWhiteSpace(slovo))
                {
                    string shablon = $@"\b{Regex.Escape(slovo.Trim())}\b";
                    string zamina = new string('*', slovo.Trim().Length);
                    text = Regex.Replace(text, shablon, zamina, RegexOptions.IgnoreCase);
                }
            }

            // Генерація шляху для результату
            string rez = Path.Combine(
                Path.GetDirectoryName(shlyahDotextu),
                Path.GetFileNameWithoutExtension(shlyahDotextu) + "_цензуровано" + Path.GetExtension(shlyahDotextu)
            );

            // Збереження результату
            File.WriteAllText(rez, text, Encoding.UTF8);
            Console.WriteLine($"Оброблений файл збережено: {rez}");
        }
        catch (FileNotFoundException pomylka)
        {
            Console.WriteLine($"Файл не знайдено - {pomylka.FileName}");
        }
        catch (Exception pomylka)
        {
            Console.WriteLine($"Сталася помилка: {pomylka.Message}");
        }
    }
}
