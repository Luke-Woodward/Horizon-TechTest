using System;
using System.IO;

namespace Horizon_TechTest
{
    class Program
    {
        static ILogParser Parser = new W3CLogParser();
        static void Main(string[] args)
        {            
            var reader = new StreamReader(@"Resources\w3clog.txt");
            string logContents = reader.ReadToEnd();
           
            var result = Parser.ParseLog(logContents);

            Console.WriteLine(result);
            Console.Read();
        }
    }
}
