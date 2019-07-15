using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Golem.MarketApi.Client.Swagger.Model
{

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class RequestorEvent {
    /// <summary>
    /// Gets or Sets EventType
    /// </summary>
    [DataMember(Name="eventType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eventType")]
    public string EventType { get; set; }

    /// <summary>
    /// Gets or Sets ProviderId
    /// </summary>
    [DataMember(Name="providerId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "providerId")]
    public string ProviderId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class RequestorEvent {\n");
      sb.Append("  EventType: ").Append(EventType).Append("\n");
      sb.Append("  ProviderId: ").Append(ProviderId).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
