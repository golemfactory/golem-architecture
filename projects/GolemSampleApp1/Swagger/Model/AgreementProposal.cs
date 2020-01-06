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
  public class AgreementProposal {
    /// <summary>
    /// id of the proposal to be promoted to the Agreement
    /// </summary>
    /// <value>id of the proposal to be promoted to the Agreement</value>
    [DataMember(Name="proposalId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "proposalId")]
    public string ProposalId { get; set; }

    /// <summary>
    /// End of validity period. Agreement needs to be accepted, rejected or cancellled before this date; otherwise will expire 
    /// </summary>
    /// <value>End of validity period. Agreement needs to be accepted, rejected or cancellled before this date; otherwise will expire </value>
    [DataMember(Name="validTo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "validTo")]
    public DateTime? ValidTo { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class AgreementProposal {\n");
      sb.Append("  ProposalId: ").Append(ProposalId).Append("\n");
      sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
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
