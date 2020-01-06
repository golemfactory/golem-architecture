using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Golem.MarketApi.Client.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class PropertyQuery : Event {
    /// <summary>
    /// Gets or Sets IssuerProperties
    /// </summary>
    [DataMember(Name="issuerProperties", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "issuerProperties")]
    public Object IssuerProperties { get; set; }

    /// <summary>
    /// Gets or Sets QueryId
    /// </summary>
    [DataMember(Name="queryId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "queryId")]
    public string QueryId { get; set; }

    /// <summary>
    /// Gets or Sets QueriedProperties
    /// </summary>
    [DataMember(Name="queriedProperties", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "queriedProperties")]
    public List<string> QueriedProperties { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class PropertyQuery {\n");
      sb.Append("  IssuerProperties: ").Append(IssuerProperties).Append("\n");
      sb.Append("  QueryId: ").Append(QueryId).Append("\n");
      sb.Append("  QueriedProperties: ").Append(QueriedProperties).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public  new string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
