using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Horizon_TechTest.Enums;

namespace Horizon_TechTest
{
    public class LogEntry
    {
        public DateTime Date;
        public string ServiceName;
        public string ServerName;
        public string ServerIp;
        public HttpVerb Method;
        public string UriStem;
        public string UriQuery;
        public ushort? ServerPort;
        public string UserName;
        public string ClientIp;
        public string ProtocolVersion;
        public string UserAgent;
        public string Cookie;
        public string Referrer;
        public string Host;
        public HttpStatusCode? StatusCode;
        public ushort? SubStatusCode;
        public string Win32Status;
        public int? BytesSent;
        public int? BytesReceived;
        public TimeSpan TimeTaken;
        public string StreamId;
    }
}
