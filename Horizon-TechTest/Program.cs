using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            var occuranceDict = CountOccurances(result);

            Console.WriteLine(result.Count);
            occuranceDict = occuranceDict.OrderBy(x => x.Value);
            foreach(var occurance in occuranceDict)
            {
                Console.WriteLine(string.Format("{0} - {1}", occurance.Key.Uri, occurance.Value));
            }

            Console.Read();
        }

        static Dictionary<LogEntry, int> CountOccurances(List<LogEntry> input)
        {
            Dictionary<LogEntry, int> occuranceDict = new Dictionary<LogEntry, int>();
            foreach (var entry in input)
            {
                if(!occuranceDict.Keys.Any(x => ((LogEntry)x).Uri == entry.Uri))
                {
                    occuranceDict.Add(entry, 1);
                }
                else
                {
                    occuranceDict[occuranceDict.SingleOrDefault(x => x.Key.Uri == entry.Uri).Key] ++;
                }
            }

            return occuranceDict;
        }
    }
}
