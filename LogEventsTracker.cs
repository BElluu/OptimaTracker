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
        readonly static string optimaPath = "Comarch\\Opt!ma\\Logs\\abc.log";
        public static List<Event> ReadOptimaLogs()
        {
            Random rnd = new Random();
            DateTime lastEventDate = new DateTime();
            //Uncomment for tests
            //DateTime lastEventDate = DateTime.ParseExact("2021-07-18 18:07:20", 
            //"yyyy-MM-dd HH:mm:ss", 
            // System.Globalization.CultureInfo.InvariantCulture);

            List<Event> trackerEvents = new List<Event>();
            /*            string[] procedures = {
                            "Logowanie", "BazLista",
                            "KreatorBazy", "CfgStanowiskoOgolneParametry",
                            "CfgSerwisOperacjiAutomatycznych" };*/

            var procedures = proceduresToArray();

            if (LogFileExist())
                try
                {
                    var logLines = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), optimaPath));
                    foreach (string line in logLines)
                    {
                        // TODO first check if date is in line, if true, check that date with lastEventDate
                        if (line.Contains("Initialized") && ParseStringToDateTime(line) > lastEventDate)
                        {
                            foreach (string procedure in procedures)
                            {
                                if (line.Contains(procedure))
                                {
                                    if (trackerEvents.Exists(x => x.procedureId == procedure))
                                    {
                                        trackerEvents.Find(p => p.procedureId == procedure).numberOfOccurrences++;
                                    }
                                    else
                                    {
                                        trackerEvents.Add(new Event()
                                        {
                                            procedureId = procedure,
                                            numberOfOccurrences = rnd.Next(1, 50)
                                        });
                                    }
                                    lastEventDate = ParseStringToDateTime(line);
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

        private static bool LogFileExist()
        {
            var logFile = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), optimaPath);
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
            XDocument procedureXml = XDocument.Load("D:\\pobrane\\OptimaTracker\\procid.xml");
            string[] procedures = procedureXml.Root.Descendants("Procedure").Select(e => e.Attribute("Name").Value).ToArray();

            return procedures;
        }
    }
}
