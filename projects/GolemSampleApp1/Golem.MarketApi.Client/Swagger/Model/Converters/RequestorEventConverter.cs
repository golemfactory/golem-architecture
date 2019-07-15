using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.MarketApi.Client.Swagger.Model.Converters
{
    public class RequestorEventConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(RequestorEvent).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            if (item.ContainsKey("eventType") && item["eventType"].Value<string>() == "offer")
            {
                return item.ToObject<OfferEvent>();
            }
            else
            {
                return item.ToObject<RequestorEvent>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
