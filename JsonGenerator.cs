using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimaTracker
{
   public class JsonGenerator
    {
        public static void Generate(List<Event> procedures) 
        {
            using (StreamWriter file = File.CreateText(@"D:\OptimaTracker.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, procedures);
            }
        }
    }
}
