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
  public class Offer : DemandOfferBase {
    /// <summary>
    /// Gets or Sets OfferId
    /// </summary>
    [DataMember(Name="offerId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "offerId")]
    public string OfferId { get; set; }

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
        sb.Append("class Offer {\n");
        sb.Append("  OfferId: ").Append(OfferId).Append("\n");
        sb.Append("  ProviderId: ").Append(ProviderId).Append("\n");
        sb.Append("  Properties: ").Append(Properties).Append("\n");
        sb.Append("  Constraints: ").Append(Constraints).Append("\n");
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
