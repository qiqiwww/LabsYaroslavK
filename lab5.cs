using System;
using System.Linq;
using System.Text;

namespace lab5
{
    enum VydAgenstva { Derzhavna, Pryvatna, Akcionerna }

    class Persona
    {
        public string Imya { get; set; }
        public string Prizvyshche { get; set; }
        public override string ToString() => $"{Imya} {Prizvyshche}";
    }

    class Neruhomist
    {
        public Persona Rieltor { get; set; }
        public string Opys { get; set; }
        public double Cina { get; set; }

        public Neruhomist()
        {
            Rieltor = new Persona();
            Opys = "-";
            Cina = 0;
        }

        public Neruhomist(Persona rieltor, string opys, double cina)
        {
            Rieltor = rieltor;
            Opys = opys;
            Cina = cina;
        }

        public override string ToString() => $"Рієлтор: {Rieltor}\nОпис: {Opys}\nВартість: {Cina:C2}";
    }

    class AhenstvoNeruhom
    {
        private string nazva;
        private VydAgenstva vyd;
        private DateTime datazasnovku;
        private double cina;
        private Neruhomist[] neruhomists = new Neruhomist[0];

        public AhenstvoNeruhom(string nazva, VydAgenstva vyd, DateTime datazasnovku, double cina)
            : this(nazva, vyd, datazasnovku, cina, Array.Empty<Neruhomist>())
        {
        }

        public AhenstvoNeruhom(string nazva, VydAgenstva vyd, DateTime datazasnovku, double cina, Neruhomist[] neruhomists)
        {
            Nazva = nazva;
            this.vyd = vyd;
            Datazasnovku = datazasnovku;
            Cina = cina;
            this.neruhomists = neruhomists ?? Array.Empty<Neruhomist>();
        }

        public AhenstvoNeruhom()
        {
            nazva = "-";
            vyd = VydAgenstva.Pryvatna;
            datazasnovku = DateTime.Today;
            cina = 0;
        }

        public string Nazva
        {
            get => nazva;
            set => nazva = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Назва не має бути порожньою");
        }

        public VydAgenstva Vyd
        {
            get => vyd;
            set => vyd = value;
        }

        public DateTime Datazasnovku
        {
            get => datazasnovku;
            set => datazasnovku = value.Date <= DateTime.Today
                ? value
                : throw new ArgumentException("Дата не може бути в майбутньому");
        }

        public double Cina
        {
            get => cina;
            set => cina = value >= 0
                ? value
                : throw new ArgumentException("Вартість має бути додатньою");
        }

        public Neruhomist[] Neruhomists
        {
            get => neruhomists;
            set => neruhomists = value ?? Array.Empty<Neruhomist>();
        }

        public double SerednyaCina => neruhomists.Length > 0
            ? neruhomists.Average(a => a.Cina)
            : 0;

        public bool this[VydAgenstva vyd] => this.vyd == vyd;

        public void DodavannyaNeruh(Neruhomist[] novaneruh)
        {
            if (novaneruh == null || novaneruh.Length == 0) return;

            int rozmir = neruhomists.Length;
            Array.Resize(ref neruhomists, rozmir + novaneruh.Length);
            novaneruh.CopyTo(neruhomists, rozmir);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Назва: {nazva}");
            sb.AppendLine($"Вид: {vyd}");
            sb.AppendLine($"Дата заснування: {datazasnovku:dd.MM.yyyy}");
            sb.AppendLine($"Вартість агенції: {cina:C2}");
            sb.AppendLine($"Середня ціна нерухомості: {SerednyaCina:C2}");
            sb.AppendLine($"Список нерухомостей:");
            foreach (var n in neruhomists)
                sb.AppendLine(n?.ToString() ?? "Об'єкт відсутній");
            return sb.ToString();
        }

        public string ToShortString() =>
            $"Назва: {nazva}, Вид: {vyd}, Дата: {datazasnovku:dd.MM.yyyy}, " +
            $"Вартість: {cina:C2}, Об'єкти: {neruhomists.Length}, " +
            $"Середня ціна: {SerednyaCina:C2}";
    }

    class Programa
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var ahenstvo = new AhenstvoNeruhom();
            Console.WriteLine("Стисла інформація:\n " + ahenstvo.ToShortString());

            Console.WriteLine($"\nПеревірка індексатора:");
            Console.WriteLine($"Akcionerna: {ahenstvo[VydAgenstva.Akcionerna]}");
            Console.WriteLine($"Derzhavna: {ahenstvo[VydAgenstva.Derzhavna]}");
            Console.WriteLine($"Pryvatna: {ahenstvo[VydAgenstva.Pryvatna]}");

            ahenstvo.Nazva = "Елітна нерухомість";
            ahenstvo.Vyd = VydAgenstva.Pryvatna;
            ahenstvo.Datazasnovku = new DateTime(2018, 3, 19);
            ahenstvo.Cina = 350000;

            Console.WriteLine("\nОновлена інформація:\n" + ahenstvo.ToString());

            var neruhomists = new[]
            {
                new Neruhomist(
                    new Persona{Imya = "Петро", Prizvyshche="Порошенко"},
                    "Маєток",
                    1200000
                ),
                new Neruhomist(
                    new Persona{Imya = "Каньє", Prizvyshche="Вест"},
                    "Будиночок",
                    1453649554
                )
            };

            ahenstvo.DodavannyaNeruh(neruhomists);
            Console.WriteLine("\nПісля додавання об'єктів:\n" + ahenstvo.ToString());

            var ahenstvo1 = new AhenstvoNeruhom(
                "Престиж",
                VydAgenstva.Pryvatna,
                new DateTime(2019, 3, 19),
                450000
            );

            var ahenstvo2 = new AhenstvoNeruhom(
                "Державна нерухомість",
                VydAgenstva.Derzhavna,
                new DateTime(2001, 7, 4),
                280000
            );

            ahenstvo1.DodavannyaNeruh(new[] {
                new Neruhomist(
                    new Persona { Imya = "Андрій", Prizvyshche = "Коваль" },
                    "Пентхаус з панорамним видом",
                    600000
                )
            });

            ahenstvo2.DodavannyaNeruh(new[] {
                new Neruhomist(
                    new Persona { Imya = "Наталія", Prizvyshche = "Шевченко" },
                    "Складське приміщення",
                    240000
                )
            });

            var ahenstva = new[] { ahenstvo, ahenstvo1, ahenstvo2 };

            Console.WriteLine("\nІнформація про агенства:");
            foreach (var ah in ahenstva)
            {
                Console.WriteLine("\n" + ah.ToShortString());

                var rieltors = ah.Neruhomists
                    .Where(n => n != null && n.Rieltor != null)
                    .Select(n => n.Rieltor.ToString())
                    .Distinct();

                Console.WriteLine("Рієлтори: " + string.Join(", ", rieltors));
            }
        }
    }
}





