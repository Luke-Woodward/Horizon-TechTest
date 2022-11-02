using System;
using System.IO;

namespace Horizon_TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(@"Resources\w3clog.txt");
            string logContents = reader.ReadToEnd();
           
            var result = W3CLogParser.ParseLog(logContents);

            Console.WriteLine(result);
            Console.Read();
        }
    }
}
