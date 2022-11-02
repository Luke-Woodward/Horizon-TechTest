using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Horizon_TechTest.Enums;

namespace Horizon_TechTest
{
    public class W3CLogParser : ILogParser
    {
        public List<LogEntry> ParseLog(string input)
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
                            datePart = fields[i];
                            break;
                        case "time":
                            timePart = fields[i];
                            break;
                        case "s-sitename":
                            entry.ServiceName = fields[i];
                            break;
                        case "s-computername":
                            entry.ServerName = fields[i];
                            break;
                        case "s-ip":
                            entry.ServerIp = fields[i];
                            break;
                        case "cs-method":
                            entry.Method = Enum.TryParse(typeof(HttpVerb), fields[i].ToUpper(), out var parsedMethod) ? (HttpVerb)parsedMethod : HttpVerb.OTHER;
                            break;
                        case "cs-uri-stem":
                            entry.UriStem = fields[i];
                            break;
                        case "cs-uri-query":
                            entry.UriQuery = fields[i];
                            break;
                        case "s-port":
                            entry.ServerPort = ushort.TryParse(fields[i], out var parsedPort) ? (ushort?)parsedPort : null;
                            break;
                        case "cs-username":
                            entry.UserName = fields[i];
                            break;
                        case "c-ip":
                            entry.ClientIp = fields[i];
                            break;
                        case "cs-version":
                            entry.ProtocolVersion = fields[i];
                            break;
                        case "cs(useragent)":
                            entry.UserAgent = fields[i];
                            break;
                        case "cs(cookie)":
                            entry.Cookie = fields[i];
                            break;
                        case "cs(referrer)":
                            entry.Referrer = fields[i];
                            break;
                        case "cs-host":
                            entry.Host = fields[i];
                            break;
                        case "sc-status":
                            entry.StatusCode = Enum.TryParse(typeof(HttpStatusCode), fields[i], out var parsedStatusCode) ? (HttpStatusCode?)parsedStatusCode : null;
                            break;
                        case "sc-substatus":
                            entry.SubStatusCode = ushort.TryParse(fields[i], out var parsedSubStatus) ? (ushort?)parsedSubStatus : null;
                            break;
                        case "sc-win32-status":
                            entry.Win32Status = fields[i];
                            break;
                        case "sc-bytes":
                            entry.BytesSent = int.TryParse(fields[i], out var parsedSBytes) ? (int?)parsedSBytes : null;
                            break;
                        case "cs-bytes":
                            entry.BytesReceived = int.TryParse(fields[i], out var parsedRBytes) ? (int?)parsedRBytes : null;
                            break;
                        case "time-taken":
                            entry.TimeTaken = TimeSpan.FromMilliseconds(Convert.ToInt32(fields[i]));
                            break;
                        case "streamid":
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
