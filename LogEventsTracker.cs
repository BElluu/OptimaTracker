using System;
using System.Collections.Generic;
using System.IO;

namespace OptimaTracker
{
    public class LogEventsTracker
    {
        readonly static string optimaPath = "Comarch\\Opt!ma\\Logs\\abc.log";
        public static List<Event> ReadOptimaLogs()
        {
            List<Event> trackerEvents = new List<Event>();
            string[] procedures = { 
                "Logowanie", "BazLista",
                "KreatorBazy", "CfgStanowiskoOgolneParametry",
                "CfgSerwisOperacjiAutomatycznych" };

            if (LogFileExist())
                try
                {
                    var logLines = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), optimaPath));
                    foreach (string line in logLines)
                    {
                        foreach (string procedure in procedures)
                        {
                            if (line.Contains(procedure) && line.Contains("Initialized"))
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
                                        numberOfOccurrences = 1
                                    });
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

        public static bool LogFileExist()
        {
            var logFile = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), optimaPath);
            if (File.Exists(logFile))
                return true;

            return false;
        }
    }
}
