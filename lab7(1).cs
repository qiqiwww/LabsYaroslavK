using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab7
{
    enum VydAgenstva { Derzhavna, Pryvatna, Akcionerna }

    class Persona : IComparable<Persona>, ICloneable
    {
        public string Imya { get; set; }
        public string Prizvyshche { get; set; }

        public override string ToString() => $"{Imya} {Prizvyshche}";

        public int CompareTo(Persona other)
        {
            if (other == null) return 1;

            int prizvComparison = string.Compare(Prizvyshche, other.Prizvyshche, StringComparison.OrdinalIgnoreCase);
            if (prizvComparison != 0) return prizvComparison;

            return string.Compare(Imya, other.Imya, StringComparison.OrdinalIgnoreCase);
        }

        public object Clone() => new Persona
        {
            Imya = this.Imya,
            Prizvyshche = this.Prizvyshche
        };
    }

    class Neruhomist : IComparable<Neruhomist>, ICloneable
    {
        public Persona Rieltor { get; set; }
        public string Opys { get; set; }
        public double Cina { get; set; }

        public Neruhomist()
        {
            Rieltor = new Persona();
            Opys = "Опис відсутній";
            Cina = 0;
        }

        public Neruhomist(Persona rieltor, string opys, double cina)
        {
            Rieltor = rieltor;
            Opys = opys;
            Cina = cina;
        }

        public override string ToString() =>
            $"Рієлтор: {Rieltor}\nОпис: {Opys}\nВартість: {Cina:C2}";

        public int CompareTo(Neruhomist other)
        {
            if (other == null) return 1;
            return Cina.CompareTo(other.Cina);
        }

        public object Clone() => new Neruhomist
        {
            Rieltor = (Persona)this.Rieltor?.Clone(),
            Opys = this.Opys,
            Cina = this.Cina
        };
    }

    interface IContainer
    {
        int Count { get; }
        object this[int index] { get; set; }
        void Add(object element);
        void Delete(object element);
    }

    interface IFileContainer : IContainer
    {
        void Save(string fileName);
        void Load(string fileName);
        bool IsDataSaved { get; }
    }

    class AhenstvoNeruhom : IFileContainer, IEnumerable, IEnumerator
    {
        private string nazva;
        private VydAgenstva vyd;
        private DateTime datazasnovku;
        private double cina;
        private Neruhomist[] neruhomists = Array.Empty<Neruhomist>();
        private bool dataSaved;
        private int position = -1;

        public IEnumerator GetEnumerator() => this;
        public bool MoveNext() => ++position < neruhomists.Length;
        public void Reset() => position = -1;
        public object Current => neruhomists[position];

        public int Count => neruhomists.Length;

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= neruhomists.Length)
                    throw new IndexOutOfRangeException("Індекс поза межами діапазону");
                return neruhomists[index];
            }
            set
            {
                if (index < 0 || index >= neruhomists.Length)
                    throw new IndexOutOfRangeException("Індекс поза межами діапазону");
                if (value is Neruhomist n)
                    neruhomists[index] = n;
                else
                    throw new ArgumentException("Невірний тип об'єкта");
            }
        }

        public void Add(object element)
        {
            if (element is not Neruhomist n)
                throw new ArgumentException("Можна додавати лише об'єкти Neruhomist");

            Array.Resize(ref neruhomists, neruhomists.Length + 1);
            neruhomists[^1] = n;
            dataSaved = false;
        }

        public void Delete(object element)
        {
            if (element is not Neruhomist n) return;

            int index = Array.IndexOf(neruhomists, n);
            if (index < 0) return;

            var newList = neruhomists.ToList();
            newList.RemoveAt(index);
            neruhomists = newList.ToArray();
            dataSaved = false;
        }

        public bool IsDataSaved => dataSaved;

        public void Save(string fileName)
        {
            using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            foreach (var n in neruhomists)
            {
                writer.WriteLine($"{n.Rieltor.Imya};{n.Rieltor.Prizvyshche};{n.Opys};{n.Cina}");
            }
            dataSaved = true;
        }

        public void Load(string fileName)
        {
            var loadedNeruhomists = new List<Neruhomist>();

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не знайдено", fileName);

            using var reader = new StreamReader(fileName, Encoding.UTF8);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;

                var parts = line.Split(';');
                if (parts.Length != 4) continue;

                loadedNeruhomists.Add(new Neruhomist(
                    new Persona { Imya = parts[0], Prizvyshche = parts[1] },
                    parts[2],
                    double.Parse(parts[3])
                ));
            }

            neruhomists = loadedNeruhomists.ToArray();
            dataSaved = false;
        }

        public AhenstvoNeruhom() : this("Без назви", VydAgenstva.Pryvatna, DateTime.Today, 0) { }

        public AhenstvoNeruhom(string nazva, VydAgenstva vyd, DateTime datazasnovku, double cina)
            : this(nazva, vyd, datazasnovku, cina, Array.Empty<Neruhomist>()) { }

        public AhenstvoNeruhom(string nazva, VydAgenstva vyd, DateTime datazasnovku, double cina, Neruhomist[] neruhomists)
        {
            Nazva = nazva;
            this.vyd = vyd;
            Datazasnovku = datazasnovku;
            Cina = cina;
            this.neruhomists = neruhomists ?? Array.Empty<Neruhomist>();
        }

        public string Nazva
        {
            get => nazva;
            set => nazva = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Назва не може бути порожньою");
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
            dataSaved = false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Назва: {nazva}");
            sb.AppendLine($"Вид: {vyd}");
            sb.AppendLine($"Дата заснування: {datazasnovku:dd.MM.yyyy}");
            sb.AppendLine($"Вартість агенції: {cina:C2}");
            sb.AppendLine($"Середня ціна нерухомості: {SerednyaCina:C2}");
            sb.AppendLine($"Кількість об'єктів: {neruhomists.Length}");
            sb.AppendLine("Список нерухомостей:");
            foreach (var n in neruhomists)
                sb.AppendLine(n?.ToString() ?? "  [Об'єкт відсутній]");
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

            var container = new AhenstvoNeruhom();

            container.Add(new Neruhomist(
                new Persona { Imya = "Олена", Prizvyshche = "Іваненко" },
                "3-кімнатна квартира в центрі",
                250000
            ));

            container.Add(new Neruhomist(
                new Persona { Imya = "Михайло", Prizvyshche = "Петренко" },
                "Офіс у бізнес-центрі",
                180000
            ));

            container.Add(new Neruhomist(
                new Persona { Imya = "Наталія", Prizvyshche = "Сидорук" },
                "Заміський котедж",
                420000
            ));

            var sortedList = container.Neruhomists.ToList();
            sortedList.Sort();
            container.Neruhomists = sortedList.ToArray();

            Console.WriteLine("Відсортований контейнер:");
            Console.WriteLine(container);

            container.Add(new Neruhomist(
                new Persona { Imya = "Андрій", Prizvyshche = "Коваль" },
                "Пентхаус з панорамним видом",
                650000
            ));

            container.Save("container_data.txt");
            Console.WriteLine("Дані збережено у файл container_data.txt");

            var newContainer = new AhenstvoNeruhom();
            for (int i = 0; i < Math.Min(2, container.Count); i++)
            {
                newContainer.Add(((Neruhomist)container[i]).Clone());
            }

            Console.WriteLine("\nНовий контейнер (перші 2 об'єкти):");
            Console.WriteLine(newContainer);

            newContainer.Save("new_container_data.txt");
            Console.WriteLine("Дані нового контейнера збережено у файл new_container_data.txt");

            Console.WriteLine("\nПеребір об'єктів за допомогою foreach:");
            foreach (Neruhomist n in newContainer)
            {
                Console.WriteLine(n);
            }

            try
            {
                var loadedContainer = new AhenstvoNeruhom();
                loadedContainer.Load("container_data.txt");
                Console.WriteLine("\nКонтейнер завантажений з файлу:");
                Console.WriteLine(loadedContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПомилка завантаження: {ex.Message}");
            }
        }
    }
}
