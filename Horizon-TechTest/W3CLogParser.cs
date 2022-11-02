using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Horizon_TechTest.Enums;

namespace Horizon_TechTest
{
    public static class W3CLogParser
    {
        public static List<LogEntry> ParseLog(string input)
        {
            var currentLogs = new List<LogEntry>();

            var inputLines = input.Split("\n");
            string[] fieldCategories = null;
            foreach (var line in inputLines)
            {
                if (line.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                {
                    //Line is a header
                    if (line.StartsWith("#fields", StringComparison.OrdinalIgnoreCase))
                    {
                        //Line is the field header
                        var headerlessLine = line.Substring(9);
                        fieldCategories = headerlessLine.Split(' ');
                    }
                    continue;
                }

                var fields = line.Split(' ');
                LogEntry entry = new LogEntry();
                var datePart = "";
                var timePart = "";
                for (int i = 0; i < fieldCategories?.Length; i++)
                {
                    if (fields[i] == "-") continue; // Ignore empty values
                    switch (fieldCategories[i].ToLower())
                    {
                        case "date":
                            Console.WriteLine(string.Format("found date {0}", fields[i]));
                            datePart = fields[i];
                            break;
                        case "time":
                            Console.WriteLine(string.Format("found time {0}", fields[i]));
                            timePart = fields[i];
                            break;
                        case "s-sitename":
                            Console.WriteLine(string.Format("found sitename {0}", fields[i]));
                            entry.ServiceName = fields[i];
                            break;
                        case "s-computername":
                            Console.WriteLine(string.Format("found computername {0}", fields[i]));
                            entry.ServerName = fields[i];
                            break;
                        case "s-ip":
                            Console.WriteLine(string.Format("found ip {0}", fields[i]));
                            entry.ServerIp = fields[i];
                            break;
                        case "cs-method":
                            Console.WriteLine(string.Format("found method {0}", fields[i]));
                            entry.Method = Enum.TryParse(typeof(HttpVerb), fields[i], out var parsedMethod) ? (HttpVerb)parsedMethod : HttpVerb.Other;
                            break;
                        case "cs-uri-stem":
                            Console.WriteLine(string.Format("found uristem {0}", fields[i]));
                            entry.UriStem = fields[i];
                            break;
                        case "cs-uri-query":
                            Console.WriteLine(string.Format("found uriquery {0}", fields[i]));
                            entry.UriQuery = fields[i];
                            break;
                        case "s-port":
                            Console.WriteLine(string.Format("found port {0}", fields[i]));
                            entry.ServerPort = ushort.TryParse(fields[i], out var parsedPort) ? (ushort?)parsedPort : null;
                            break;
                        case "cs-username":
                            Console.WriteLine(string.Format("found username {0}", fields[i]));
                            entry.UserName = fields[i];
                            break;
                        case "c-ip":
                            Console.WriteLine(string.Format("found ip {0}", fields[i]));
                            entry.ClientIp = fields[i];
                            break;
                        case "cs-version":
                            Console.WriteLine(string.Format("found version {0}", fields[i]));
                            entry.ProtocolVersion = fields[i];
                            break;
                        case "cs(useragent)":
                            Console.WriteLine(string.Format("found agent {0}", fields[i]));
                            entry.UserAgent = fields[i];
                            break;
                        case "cs(cookie)":
                            Console.WriteLine(string.Format("found cookie {0}", fields[i]));
                            entry.Cookie = fields[i];
                            break;
                        case "cs(referrer)":
                            Console.WriteLine(string.Format("found referrer {0}", fields[i]));
                            entry.Referrer = fields[i];
                            break;
                        case "cs-host":
                            Console.WriteLine(string.Format("found host {0}", fields[i]));
                            entry.Host = fields[i];
                            break;
                        case "sc-status":
                            Console.WriteLine(string.Format("found status {0}", fields[i]));
                            entry.StatusCode = Enum.TryParse(typeof(HttpStatusCode), fields[i], out var parsedStatusCode) ? (HttpStatusCode?)parsedStatusCode : null;
                            break;
                        case "sc-substatus":
                            Console.WriteLine(string.Format("found substatus {0}", fields[i]));
                            entry.SubStatusCode = ushort.TryParse(fields[i], out var parsedSubStatus) ? (ushort?)parsedSubStatus : null;
                            break;
                        case "sc-win32-status":
                            Console.WriteLine(string.Format("found win32status {0}", fields[i]));
                            entry.Win32Status = fields[i];
                            break;
                        case "sc-bytes":
                            Console.WriteLine(string.Format("found sentbytes {0}", fields[i]));
                            entry.BytesSent = int.TryParse(fields[i], out var parsedSBytes) ? (int?)parsedSBytes : null;
                            break;
                        case "cs-bytes":
                            Console.WriteLine(string.Format("found receivedbytes {0}", fields[i]));
                            entry.BytesReceived = int.TryParse(fields[i], out var parsedRBytes) ? (int?)parsedRBytes : null;
                            break;
                        case "time-taken":
                            Console.WriteLine(string.Format("found timetaken {0}", fields[i]));
                            entry.TimeTaken = TimeSpan.FromMilliseconds(Convert.ToInt32(fields[i]));
                            break;
                        case "streamid":
                            Console.WriteLine(string.Format("found streamid {0}", fields[i]));
                            entry.StreamId = fields[i];
                            break;
                    }
                }
                var dateValid = DateTime.TryParse(string.Format("{0} {1}", datePart, timePart), out entry.Date);

                currentLogs.Add(entry);
            }
            return currentLogs;
        }
    }
}
