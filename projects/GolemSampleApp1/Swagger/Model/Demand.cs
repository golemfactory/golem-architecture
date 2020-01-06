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
  public class Demand : DemandOfferBase {
    /// <summary>
    /// Gets or Sets DemandId
    /// </summary>
    [DataMember(Name="demandId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "demandId")]
    public string DemandId { get; set; }

    /// <summary>
    /// Gets or Sets RequestorId
    /// </summary>
    [DataMember(Name="requestorId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "requestorId")]
    public string RequestorId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Demand {\n");
      sb.Append("  DemandId: ").Append(DemandId).Append("\n");
      sb.Append("  RequestorId: ").Append(RequestorId).Append("\n");
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
