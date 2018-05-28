using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regards.Solr.Net.Test.Document
{
    public class ToneSetConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = new
            {
                set = value
            };
            var json = JsonConvert.SerializeObject(obj);

            writer.WriteRawValue(json);
            //writer.WriteValue(json);
        }
    }
}
