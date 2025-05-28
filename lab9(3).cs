using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;

public class Pisnya
{
    public string Nazva { get; set; }
    public string Aftor { get; set; }
    public string Compozytor { get; set; }
    public int Rik { get; set; }
    public string Textpisni { get; set; }
    public List<string> Vykonavci { get; set; } = new List<string>();
}

public class ColekciaPisen
{
    private List<Pisnya> pisni = new List<Pisnya>();

    
    public void Dodaty(Pisnya pisnya) => pisni.Add(pisnya);//dodaty pisnyu

    
    public bool Vydalyty(string nazva, string aftor)//vydalyty pisnyu
    {
        var pisnya = pisni.FirstOrDefault(p => p.Nazva == nazva && p.Aftor == aftor);
        return pisnya != null && pisni.Remove(pisnya);
    }

    
    public List<Pisnya> Poshuk(Func<Pisnya, bool> predicate) => pisni.Where(predicate).ToList();//poshuk

    
    public void Zberehty(string shlyah)//zberehtu colekciyu u fayil
    {
        var json = JsonSerializer.Serialize(pisni, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(shlyah, json);
    }

    
    public void Zavantazhyty(string shlyah)//zavantazhyty colekciyu z failyu
    {
        var json = File.ReadAllText(shlyah);
        pisni = JsonSerializer.Deserialize<List<Pisnya>>(json);
    }

    
    public List<Pisnya> PoshukZavykonavcem(string vykonavec) =>//poshuk za vyconavcem
        pisni.Where(s => s.Vykonavci.Any(p => p.Equals(vykonavec, StringComparison.OrdinalIgnoreCase))).ToList();
}

public class Program
{
    static ColekciaPisen colekciya = new ColekciaPisen();
    static string fayilzinfo = "pisni.json";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("\n1. Додати пісню\n2. Видалити пісню\n3. Пошук пісень\n4. Зберегти колекцію\n5. Завантажити колекцію\n6. Показати всі пісні виконавця\n0. Вихід");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Dodaty(); break;
                case "2": Vydalyty(); break;
                case "3": Poshuk(); break;
                case "4": colekciya.Zberehty(fayilzinfo); break;
                case "5": Zavantazhyty(); break;
                case "6": PokazatyZavykonavcem(); break;
                case "0": return;
                default: Console.WriteLine("Хибний вибір"); break;
            }
        }
    }

    static void Dodaty()
    {
        var song = new Pisnya();
        Console.Write("Назва: "); song.Nazva = Console.ReadLine();
        Console.Write("Автор: "); song.Aftor = Console.ReadLine();
        Console.Write("Композитор: "); song.Compozytor = Console.ReadLine();
        Console.Write("Рік: "); song.Rik = int.Parse(Console.ReadLine());
        Console.Write("Текст: "); song.Textpisni = Console.ReadLine();

        Console.WriteLine("Виконавці:");
        song.Vykonavci = Console.ReadLine().Split(',').Select(p => p.Trim()).ToList();

        colekciya.Dodaty(song);
    }

    static void Vydalyty()
    {
        Console.Write("Назва: "); var nazva = Console.ReadLine();
        Console.Write("Автор: "); var aftor = Console.ReadLine();
        if (colekciya.Vydalyty(nazva, aftor))
            Console.WriteLine("Пісня видалена!");
        else
            Console.WriteLine("Пісня не знайдена!");
    }

    static void Poshuk()
    {
        Console.Write("Пошук за (1-назвою, 2-автором, 3-роком): ");
        var vibir = Console.ReadLine();
        IEnumerable<Pisnya> rez = null;

        switch (vibir)
        {
            case "1":
                Console.Write("Назва: ");
                rez = colekciya.Poshuk(s => s.Nazva.Contains(Console.ReadLine()));
                break;
            case "2":
                Console.Write("Автор: ");
                rez = colekciya.Poshuk(s => s.Aftor.Contains(Console.ReadLine()));
                break;
            case "3":
                Console.Write("Рік: ");
                rez = colekciya.Poshuk(s => s.Rik == int.Parse(Console.ReadLine()));
                break;
        }

        foreach (var pisnya in rez)
            Console.WriteLine($"\n{pisnya.Nazva} ({pisnya.Rik})\nАвтор: {pisnya.Aftor}\nВиконавці: {string.Join(", ", pisnya.Vykonavci)}");
    }

    static void Zavantazhyty()
    {
        try
        {
            colekciya.Zavantazhyty(fayilzinfo);
            Console.WriteLine("Колекція завантажена!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не знайдено!");
        }
    }

    static void PokazatyZavykonavcem()
    {
        Console.Write("Виконавець: ");
        var pisni = colekciya.PoshukZavykonavcem(Console.ReadLine());
        foreach (var pisnya in pisni)
            Console.WriteLine($"\n{pisnya.Nazva} ({pisnya.Rik})");
    }
}
