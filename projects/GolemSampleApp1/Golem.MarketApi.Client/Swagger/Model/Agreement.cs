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
  public class Agreement {
    /// <summary>
    /// Gets or Sets ProposalId
    /// </summary>
    [DataMember(Name="proposalId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "proposalId")]
    public string ProposalId { get; set; }

    /// <summary>
    /// Gets or Sets ExpirationDate
    /// </summary>
    [DataMember(Name="expirationDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "expirationDate")]
    public DateTime? ExpirationDate { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Agreement {\n");
      sb.Append("  ProposalId: ").Append(ProposalId).Append("\n");
      sb.Append("  ExpirationDate: ").Append(ExpirationDate).Append("\n");
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
