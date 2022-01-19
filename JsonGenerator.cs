using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OptimaTracker
{
    public class JsonGenerator
    {
        public static void GenerateJsonFile(string serialKey, string TIN, List<Event> procedures)
        {
            var jsonObject = new Company
            {
                serialKey = serialKey,
                TIN = TIN,
                events = procedures
            };

            using (StreamWriter file = File.CreateText(@"Z:\OptimaTracker\OptimaTracker.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObject);
            }
        }

        public static string GenerateJsonData(string serialKey, string TIN ,List<Event> procedures)
        {
            var jsonData = JsonConvert.SerializeObject(new Company
            {
                serialKey = serialKey, //Klucz na którym zalogowany jest klient
                TIN = TIN, // NIP z pieczątki firmy
                events = procedures
            });

            return jsonData;
        }
    }
}
