using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hoffman.Data
{
    class Program
    {
        static void Main(string[] args)
        {

            string source = @"..\source.txt";
            string target = @"..\target.txt";

            string text = File.ReadAllText(source);


            HoffmanAlgorithm algorithm = new HoffmanAlgorithm();

            // Кодирование           
            string coding = algorithm.CodingOutput(text);

            // Декодирование
            string decoding = algorithm.DecodingOutput(coding);
            
            using (StreamWriter sw = new StreamWriter(target, false, System.Text.Encoding.Default))
            {
                sw.Write(coding);
            }

            ShowStatistics(source, target, text, coding, decoding);

            Console.ReadLine();
        }

        private static void ShowStatistics(string source, string target, string text, string coding, string decoding)
        {
            Console.WriteLine($"-------------------Исходный текст------------------");
            Console.WriteLine($"{text}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"-----------------Кодированный текст----------------");
            Console.WriteLine($"{coding}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"--------------Раскодированный текст----------------");
            Console.WriteLine($"{decoding}");
            Console.WriteLine(new string('-', 50));
            FileInfo file = new FileInfo(source);
            Console.WriteLine($"Размер исходного файла: {file.Length} байт.");
            file = new FileInfo(target);
            Console.WriteLine($"Размер закодированного файла: {file.Length} байт.");
        }
    }
}