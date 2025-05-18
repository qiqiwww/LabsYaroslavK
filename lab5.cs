using System.Text;

enum VydAgenstva { Derzhavnyi, Pryvatnyi, Aktsionernyi }

class Building
{
    private string nazvaAgenstva;
    private VydAgenstva typAgenstva;
    private DateTime dataZasnovku;
    private double vartistAgenstva;
    private Neruhomist[] proponovaNeruhomist;

    
    public Building(string nazva, VydAgenstva typ, DateTime data, double vartist)//з параметрами
    {
        this.nazvaAgenstva = nazva;
        this.typAgenstva = typ;
        this.dataZasnovku = data;
        this.vartistAgenstva = vartist;
        this.proponovaNeruhomist = new Neruhomist[325];
    }

    
    public Building()//без параметрів
    {
        nazvaAgenstva = "Агенство";
        typAgenstva = VydAgenstva.Pryvatnyi;
        dataZasnovku = DateTime.Now;
        vartistAgenstva = 0;
        proponovaNeruhomist = new Neruhomist[325];
    }

    
    public override string ToString()
    {
        return $"Назва агенства нерухомості: {nazvaAgenstva} \nТип: {typAgenstva} " +
               $"\nДата заснування: {dataZasnovku.ToShortDateString()} " +
               $"\nВартість: {vartistAgenstva:C2} \nКількість об'єктів: {proponovaNeruhomist.Length}";
    }

    
    public string ToShortString()
    {
        return $"Назва: {nazvaAgenstva}, Тип: {typAgenstva}, Кількість об'єктів: {proponovaNeruhomist.Length}";
    }

    
    public string Nazva
    {
        get { return nazvaAgenstva; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                nazvaAgenstva = value;
            else
                throw new ArgumentOutOfRangeException();
        }
    }

    public VydAgenstva Typ
    {
        get { return typAgenstva; }
        set { typAgenstva = value; }
    }

    public DateTime DataZasnovku
    {
        get { return dataZasnovku; }
        set { dataZasnovku = value; }
    }

    public double Vartist
    {
        get { return vartistAgenstva; }
        set
        {
            if (value >= 0)
                vartistAgenstva = value;
            else
                throw new ArgumentOutOfRangeException();
        }
    }

   
    public void DodatyNeruhomist(Neruhomist[] neruhomist)//додавання нерухомості до списку
    {
        if (neruhomist != null)
        {
            var novyiSpysok = new Neruhomist[proponovaNeruhomist.Length + neruhomist.Length];
            proponovaNeruhomist.CopyTo(novyiSpysok, 0);
            neruhomist.CopyTo(novyiSpysok, proponovaNeruhomist.Length);
            proponovaNeruhomist = novyiSpysok;
        }
    }
}


class Neruhomist//додатковий клас для нерухомості
{
    public string Rieltor { get; set; }
    public double SeredniaCina { get; set; }
    public DateTime DataListynhu { get; set; }

    public override string ToString()
    {
        return $"Рієлтор: {Rieltor}, Середньозважена ціна: {SeredniaCina:C2}, Дата лістингу: {DataListynhu.ToShortDateString()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        Building ahenstvoDerzhavnoiNeruhomosti = new Building("THE CAPITAL", VydAgenstva.Pryvatnyi, DateTime.Now.AddYears(-7), 500000);

        
        Neruhomist[] neruhomist = new Neruhomist[] //масиву нерухомості
        {
            new Neruhomist { Rieltor = "Радіщева Єлизавета", SeredniaCina = 300000, DataListynhu = DateTime.Now.AddMonths(-2) },
            new Neruhomist { Rieltor = "Кисельов Костянтин", SeredniaCina = 250000, DataListynhu = DateTime.Now.AddMonths(-1) }
        };

        
        ahenstvoDerzhavnoiNeruhomosti.DodatyNeruhomist(neruhomist);

        
        Console.WriteLine(ahenstvoDerzhavnoiNeruhomosti.ToString()); //виведення інформації
       // Console.WriteLine(ahenstvoDerzhavnoiNeruhomosti.ToShortString());

        
        Console.WriteLine("\nЗначення індексатора(тип): "); //індексатор
        Console.WriteLine(ahenstvoDerzhavnoiNeruhomosti.Typ == VydAgenstva.Pryvatnyi ? "true" : "false");
    }
}
