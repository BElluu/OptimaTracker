using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OptimaTracker
{
    public class LogEventsTracker
    {
        public static List<Event> ReadOptimaLogs(string pathToLogs, DateTime lastEventSendDate)
        {
            Random rnd = new Random();
            //Uncomment for tests
            //DateTime lastEventDate = DateTime.ParseExact("2021-07-18 18:07:20", 
            //"yyyy-MM-dd HH:mm:ss", 
            // System.Globalization.CultureInfo.InvariantCulture);

            List<Event> trackerEvents = new List<Event>();

            var procedures = proceduresToArray();

            if (LogFileExist(pathToLogs))
                try
                {
                    var logLines = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), pathToLogs));
                    foreach (string line in logLines)
                    {
                        // TODO in ver2 - first check if date is in line, if true, check that date with lastEventDate
                        if (line.Contains("Initialized") && ParseStringToDateTime(line) > lastEventSendDate)
                        {
                            foreach (string procedure in procedures)
                            {
                                if (line.Contains(procedure))
                                {
                                    if (trackerEvents.Exists(x => x.procedureName == procedure))
                                    {
                                        trackerEvents.Find(p => p.procedureName == procedure).numberOfOccurrences++;
                                    }
                                    else
                                    {
                                        trackerEvents.Add(new Event()
                                        {
                                            procedureName = procedure,
                                            numberOfOccurrences = 1
                                        });
                                    }
                                    lastEventSendDate = ParseStringToDateTime(line);
                                }
                            }
                        }
                    }

                }
                catch
                {
                    Console.WriteLine("Something went wrong...");
                }
            return trackerEvents;
        }

        private static bool LogFileExist(string pathToLogs)
        {
            var logFile = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), pathToLogs);
            if (File.Exists(logFile))
                return true;

            return false;
        }

        private static DateTime ParseStringToDateTime(string line)
        {
            Regex regex = new Regex("[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}");
            DateTime dateTime = DateTime.ParseExact(regex.Match(line).ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            return dateTime;
        }

        private static string[] proceduresToArray()
        {
            XDocument procedureXml = XDocument.Load("procedures.xml");
            string[] procedures = procedureXml.Root.Descendants("Procedure").Select(e => e.Attribute("Name").Value).ToArray();

            return procedures;
        }
    }
}
