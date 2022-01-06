using Newtonsoft.Json;
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

            using (StreamWriter file = File.CreateText(@"D:\OptimaTracker.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObject);
            }
        }

        public static string GenerateJsonData(List<Event> procedures)
        {
            var jsonData = JsonConvert.SerializeObject(new Company
            {
                serialKey = "5000012320",
                TIN = "123-123-99-88",
                events = procedures
            });

            return jsonData;
        }

            /*        public static List<Company> CompanyData()
                    {
                        List<Company> company = new List<Company>();
                        company.Add(new Company
                        {
                            serialKey = "5000065720",
                            TIN = "222-123-12-22"
                        });

                        return company;
                    }*/
        }
}
