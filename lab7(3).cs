using System;
using System.Text;

namespace lab7
{
    public interface ICipher
    {
        string encode(string input);
        string decode(string input);
    }

    public class ACipher : ICipher
    {
        private const string AlphabetUpper = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        private const string AlphabetLower = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

        public string encode(string input)
        {
            return Transform(input, shift: 1);
        }

        public string decode(string input)
        {
            return Transform(input, shift: -1);
        }

        private string Transform(string input, int shift)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                int upperIndex = AlphabetUpper.IndexOf(c);
                int lowerIndex = AlphabetLower.IndexOf(c);

                if (upperIndex >= 0)
                {
                    int newIndex = (upperIndex + shift + AlphabetUpper.Length) % AlphabetUpper.Length;
                    if (newIndex < 0) newIndex += AlphabetUpper.Length;
                    result.Append(AlphabetUpper[newIndex]);
                }
                else if (lowerIndex >= 0)
                {
                    int newIndex = (lowerIndex + shift + AlphabetLower.Length) % AlphabetLower.Length;
                    if (newIndex < 0) newIndex += AlphabetLower.Length;
                    result.Append(AlphabetLower[newIndex]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }

    public class BCipher : ICipher
    {
        private const string AlphabetUpper = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        private const string AlphabetLower = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

        public string encode(string input)
        {
            return Transform(input);
        }

        public string decode(string input)
        {
            return Transform(input);
        }

        private string Transform(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                int upperIndex = AlphabetUpper.IndexOf(c);
                int lowerIndex = AlphabetLower.IndexOf(c);

                if (upperIndex >= 0)
                {
                    result.Append(AlphabetUpper[AlphabetUpper.Length - 1 - upperIndex]);
                }
                else if (lowerIndex >= 0)
                {
                    result.Append(AlphabetLower[AlphabetLower.Length - 1 - lowerIndex]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            ICipher aCipher = new ACipher();
            string original = "АБВГҐДЕЄЖЗИІЇЙК ЛМНОП РСТУФХЦЧШЩЬЮЯ";

            Console.WriteLine("ACipher Шифрування:");
            string encodedA = aCipher.encode(original);
            Console.WriteLine("Оригінал: " + original);
            Console.WriteLine("Зашифровано: " + encodedA);

            Console.WriteLine("\nACipher Дешифрування:");
            string decodedA = aCipher.decode(encodedA);
            Console.WriteLine("Зашифрований текст: " + encodedA);
            Console.WriteLine("Розшифровано: " + decodedA);

            ICipher bCipher = new BCipher();
            Console.WriteLine("\n\nBCipher Шифрування:");
            string encodedB = bCipher.encode(original);
            Console.WriteLine("Оригінал: " + original);
            Console.WriteLine("Зашифровано: " + encodedB);

            Console.WriteLine("\nBCipher Дешифрування:");
            string decodedB = bCipher.decode(encodedB);
            Console.WriteLine("Зашифрований текст: " + encodedB);
            Console.WriteLine("Розшифровано: " + decodedB);

            Console.WriteLine("\nСпеціальні тести для BCipher:");
            Console.WriteLine("'А' = '" + bCipher.encode("А") + "'");
            Console.WriteLine("'Б' = '" + bCipher.encode("Б") + "'");
            Console.WriteLine("'Ґ' = '" + bCipher.encode("Ґ") + "'");
            Console.WriteLine("'а' = '" + bCipher.encode("а") + "'");
            Console.WriteLine("'є' = '" + bCipher.encode("є") + "'");
        }
    }
}
