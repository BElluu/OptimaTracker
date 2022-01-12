using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OptimaTracker
{
    public class JsonGenerator
    {
        public static void GenerateJsonFile(List<Event> procedures)
        {
            var jsonObject = new Company
            {
                serialKey = "5000012320",
                TIN = "123-123-99-88",
                events = procedures
            };

            using (StreamWriter file = File.CreateText(@"Z:\OptimaTracker\OptimaTracker.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObject);
            }
        }

        public static string GenerateJsonData(List<Event> procedures)
        {
            var jsonData = JsonConvert.SerializeObject(new Company
            {
                serialKey = GenerateSerialKey(), //Klucz na którym zalogowany jest klient
                TIN = GenerateTIN(), // NIP z pieczątki firmy
                events = procedures
            });

            return jsonData;
        }

        private static string GenerateSerialKey()
        {
            Random rnd = new Random();
            return rnd.Next(500000000, 500999999).ToString();
        }

        private static string GenerateTIN()
        {
            Random rnd = new Random();
            return rnd.Next(111111111, 999999999).ToString();
        }
    }
}
