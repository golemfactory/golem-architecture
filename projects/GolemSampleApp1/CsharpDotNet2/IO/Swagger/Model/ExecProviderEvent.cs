using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ExecProviderEvent : ProviderEvent {
    /// <summary>
    /// Gets or Sets BatchId
    /// </summary>
    [DataMember(Name="batchId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "batchId")]
    public string BatchId { get; set; }

    /// <summary>
    /// Gets or Sets ExeScript
    /// </summary>
    [DataMember(Name="exeScript", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "exeScript")]
    public ExeScriptBatch ExeScript { get; set; }

    /// <summary>
    /// Gets or Sets EventType
    /// </summary>
    [DataMember(Name="eventType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eventType")]
    public string EventType { get; set; }

    /// <summary>
    /// Gets or Sets ActivityId
    /// </summary>
    [DataMember(Name="activityId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "activityId")]
    public string ActivityId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ExecProviderEvent {\n");
      sb.Append("  BatchId: ").Append(BatchId).Append("\n");
      sb.Append("  ExeScript: ").Append(ExeScript).Append("\n");
      sb.Append("  EventType: ").Append(EventType).Append("\n");
      sb.Append("  ActivityId: ").Append(ActivityId).Append("\n");
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
