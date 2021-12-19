using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OptimaTracker
{
    public class LogEventsTracker
    {
        readonly static string optimaPath = "Comarch\\Opt!ma\\Logs\\abc.log";
        public static List<Event> ReadOptimaLogs()
        {
            List<Event> jsonData = new List<Event>();
            string[] procedures = { "Logowanie", "BazLista" };
            if(LogFileExist())
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
                                jsonData.Add(new Event()
                                {
                                    procedureId = procedure,
                                    numberOfOccurrences = 1
                                });
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Something went wrong...");
                }
            return jsonData;
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
