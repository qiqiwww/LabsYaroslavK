using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Predmet//objecty vmistu
{
    public string Nazva { get; set; }
    public double Objem { get; set; }
}


public class PerevyshchennyaObsyahu : Exception//u razi perevyshchennya
{
    public PerevyshchennyaObsyahu(string povidomlennya) : base(povidomlennya) { }
}

public class Valiza
{

    public string Kolir { get; set; }//vlastyvosti
    public string Vyrobnyk { get; set; }
    public double Vaha { get; set; }
    public double Objem { get; set; }
    public List<Predmet> Predmety { get; } = new List<Predmet>();


    public delegate void DodaavannyaPredmeta(object dzherelo, Predmet predmet);//dodavannya objecta
    public event DodaavannyaPredmeta DodaavannyaPredmet;


    public void DodatyPredmet(Predmet predmet)//metod dodayannya objecta
    {
        double sumaObjemiv = Predmety.Sum(p => p.Objem) + predmet.Objem;

        if (sumaObjemiv > Objem)
            throw new PerevyshchennyaObsyahu("Неможливо додати предмет: перевищено об'єм валізи");

        Predmety.Add(predmet);
        DodaavannyaPredmet?.Invoke(this, predmet); //vyklyk podyiyi
    }


    public void ZapovnennyaHarakterystyk(string kolir, string vyrobnyk, double vaha, double objem)//metod zapovnennya harakterystyk
    {
        Kolir = kolir;
        Vyrobnyk = vyrobnyk;
        Vaha = vaha;
        Objem = objem;
    }
}


class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Valiza valiza = new Valiza();
        valiza.ZapovnennyaHarakterystyk("Чорний", "Xiaomi Front Opening Luggage 20", 4, 30);

        // Підписка на подію
        valiza.DodaavannyaPredmet += (dzherelo, predmet) =>
            Console.WriteLine($"Додано предмет: {predmet.Nazva} (Об'єм: {predmet.Objem})");

        try
        {
            valiza.DodatyPredmet(new Predmet { Nazva = "Ноутбук", Objem = 5 });
            valiza.DodatyPredmet(new Predmet { Nazva = "Одяг", Objem = 15 });
            valiza.DodatyPredmet(new Predmet { Nazva = "Книги", Objem = 12 });
        }
        catch (PerevyshchennyaObsyahu ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
