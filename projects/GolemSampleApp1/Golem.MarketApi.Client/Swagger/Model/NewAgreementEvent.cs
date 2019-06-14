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
  public class NewAgreementEvent : ProviderEvent {
    /// <summary>
    /// Gets or Sets RequestorId
    /// </summary>
    [DataMember(Name="requestorId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "requestorId")]
    public string RequestorId { get; set; }

    /// <summary>
    /// Gets or Sets Demand
    /// </summary>
    [DataMember(Name="demand", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "demand")]
    public Object Demand { get; set; }

    /// <summary>
    /// Gets or Sets ProviderId
    /// </summary>
    [DataMember(Name="providerId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "providerId")]
    public string ProviderId { get; set; }

    /// <summary>
    /// Gets or Sets Offer
    /// </summary>
    [DataMember(Name="offer", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "offer")]
    public Object Offer { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class NewAgreementEvent {\n");
      sb.Append("  RequestorId: ").Append(RequestorId).Append("\n");
      sb.Append("  Demand: ").Append(Demand).Append("\n");
      sb.Append("  ProviderId: ").Append(ProviderId).Append("\n");
      sb.Append("  Offer: ").Append(Offer).Append("\n");
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
