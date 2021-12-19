using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OptimaTracker
{
    public class LogEventsTracker
    {
        readonly static string optimaPath = "Comarch\\Opt!ma\\Logs\\abc.log";
        public static void ReadOptimaLogs()
        {
            string[] procedures = { "Logowanie", "BazLista" };
            if(LogFileExist())
                try
                {
                    var logLines = File.ReadAllLines(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), optimaPath));
                    int i = 0;
                    foreach (string line in logLines)
                    {
                        foreach (string procedureId in procedures)
                        {
                            if (line.Contains(procedureId) && line.Contains("Initialized"))
                            {
                                i++;
                                Console.WriteLine(procedureId + ' ' + i);
                            }
                        }
                    }
                   // Console.WriteLine(label + ' ' + i);
                }
                catch
                {
                    Console.WriteLine("Something went wrong...");
                }
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
