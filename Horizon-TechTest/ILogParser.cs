using System;
using System.Collections.Generic;
using System.Text;

namespace Horizon_TechTest
{
    public interface ILogParser
    {
        public List<LogEntry> ParseLog(string input);
    }
}
