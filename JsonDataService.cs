using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CableSubscriberApp
{
    public class JsonDataService
    {
        private const string FileName = "subscribers.json";

        public List<Subscriber> Load()
        {
            if (!File.Exists(FileName))
                return new List<Subscriber>();

            var json = File.ReadAllText(FileName);
            return JsonConvert.DeserializeObject<List<Subscriber>>(json)
                   ?? new List<Subscriber>();
        }

        public void Save(IEnumerable<Subscriber> subscribers)
        {
            var json = JsonConvert.SerializeObject(subscribers, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
