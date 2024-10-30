using Counter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Counter.Services
{
    public class CounterService
    {
        private const string FileName = "counters.xml";

        public async Task<List<CounterModel>> LoadCountersAsync()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);

            if (!File.Exists(path))
                return new List<CounterModel>();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(List<CounterModel>));
                return (List<CounterModel>)serializer.Deserialize(stream);
            }
        }

        public async Task SaveCountersAsync(List<CounterModel> counters)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(List<CounterModel>));
                serializer.Serialize(stream, counters);
            }
        }

    }
}
