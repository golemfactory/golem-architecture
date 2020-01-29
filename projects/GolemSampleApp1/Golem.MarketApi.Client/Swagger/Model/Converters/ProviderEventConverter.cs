using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.MarketApi.Client.Swagger.Model.Converters
{
    public class ProviderEventConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Event).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            if (item.ContainsKey("eventType") && item["eventType"].Value<string>() == "ProposalEvent")
            {
                return item.ToObject<ProposalEvent>();
            }
            if (item.ContainsKey("eventType") && item["eventType"].Value<string>() == "PropertyQueryEvent")
            {
                return item.ToObject<PropertyQueryEvent>();
            }
            if (item.ContainsKey("eventType") && item["eventType"].Value<string>() == "AgreementEvent")
            {
                return item.ToObject<AgreementEvent>();
            }
            else
            {
                return item.ToObject<Event>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
