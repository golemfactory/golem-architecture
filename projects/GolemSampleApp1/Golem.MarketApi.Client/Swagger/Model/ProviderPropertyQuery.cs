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
  public class ProviderPropertyQuery : ProviderEvent {
    /// <summary>
    /// Gets or Sets RequestorDesc
    /// </summary>
    [DataMember(Name="requestorDesc", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "requestorDesc")]
    public Object RequestorDesc { get; set; }

    /// <summary>
    /// Gets or Sets QueryId
    /// </summary>
    [DataMember(Name="queryId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "queryId")]
    public string QueryId { get; set; }

    /// <summary>
    /// Gets or Sets QueriedProps
    /// </summary>
    [DataMember(Name="queriedProps", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "queriedProps")]
    public List<string> QueriedProps { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ProviderPropertyQuery {\n");
      sb.Append("  RequestorDesc: ").Append(RequestorDesc).Append("\n");
      sb.Append("  QueryId: ").Append(QueryId).Append("\n");
      sb.Append("  QueriedProps: ").Append(QueriedProps).Append("\n");
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
