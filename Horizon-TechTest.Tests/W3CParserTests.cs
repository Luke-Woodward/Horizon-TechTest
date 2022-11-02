using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Horizon_TechTest.Tests
{
    [TestClass]
    public class W3CParserTests
    {

        [TestMethod]
        public void ParseCount()
        {
            W3CLogParser parser = new W3CLogParser();
            var reader = new StreamReader(@"Resources\w3clog.txt");
            string logContents = reader.ReadToEnd();

            var result = parser.ParseLog(logContents);

            Assert.AreEqual(result.Count, 5);
        }
        [TestMethod]
        public void ParseDateTime()
        {
            W3CLogParser parser = new W3CLogParser();
            var reader = new StreamReader(@"Resources\w3clog.txt");
            string logContents = reader.ReadToEnd();

            var result = parser.ParseLog(logContents);

            Assert.AreEqual(result[0].Date, new DateTime(1996, 01, 01, 10, 48, 2));
        }
        [TestMethod]
        public void ParseMethod()
        {
            W3CLogParser parser = new W3CLogParser();
            var reader = new StreamReader(@"Resources\w3clog.txt");
            string logContents = reader.ReadToEnd();

            var result = parser.ParseLog(logContents);

            Assert.AreEqual(result[0].Method, Enums.HttpVerb.GET);
        }
    }
}
