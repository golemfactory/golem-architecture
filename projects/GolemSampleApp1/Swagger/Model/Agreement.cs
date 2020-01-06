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
  public class Agreement {
    /// <summary>
    /// Gets or Sets AgreementId
    /// </summary>
    [DataMember(Name="agreementId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "agreementId")]
    public string AgreementId { get; set; }

    /// <summary>
    /// Gets or Sets Demand
    /// </summary>
    [DataMember(Name="demand", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "demand")]
    public Demand Demand { get; set; }

    /// <summary>
    /// Gets or Sets Offer
    /// </summary>
    [DataMember(Name="offer", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "offer")]
    public Offer Offer { get; set; }

    /// <summary>
    /// End of validity period. Agreement needs to be accepted, rejected or cancellled before this date; otherwise will expire 
    /// </summary>
    /// <value>End of validity period. Agreement needs to be accepted, rejected or cancellled before this date; otherwise will expire </value>
    [DataMember(Name="validTo", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "validTo")]
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// date of the Agreement approval
    /// </summary>
    /// <value>date of the Agreement approval</value>
    [DataMember(Name="approvedDate", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "approvedDate")]
    public DateTime? ApprovedDate { get; set; }

    /// <summary>
    /// * `Proposal` - newly created by a Requestor (based on Proposal) * `Pending` - confirmed by a Requestor and send to Provider for approval * `Cancelled` by a Requestor * `Rejected` by a Provider * `Approved` by both sides * `Expired` - not accepted, rejected nor cancelled within validity period * `Terminated` - finished after approval. 
    /// </summary>
    /// <value>* `Proposal` - newly created by a Requestor (based on Proposal) * `Pending` - confirmed by a Requestor and send to Provider for approval * `Cancelled` by a Requestor * `Rejected` by a Provider * `Approved` by both sides * `Expired` - not accepted, rejected nor cancelled within validity period * `Terminated` - finished after approval. </value>
    [DataMember(Name="state", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "state")]
    public string State { get; set; }

    /// <summary>
    /// Gets or Sets ProposedSignature
    /// </summary>
    [DataMember(Name="proposedSignature", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "proposedSignature")]
    public string ProposedSignature { get; set; }

    /// <summary>
    /// Gets or Sets ApprovedSignature
    /// </summary>
    [DataMember(Name="approvedSignature", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "approvedSignature")]
    public string ApprovedSignature { get; set; }

    /// <summary>
    /// Gets or Sets CommittedSignature
    /// </summary>
    [DataMember(Name="committedSignature", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "committedSignature")]
    public string CommittedSignature { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Agreement {\n");
      sb.Append("  AgreementId: ").Append(AgreementId).Append("\n");
      sb.Append("  Demand: ").Append(Demand).Append("\n");
      sb.Append("  Offer: ").Append(Offer).Append("\n");
      sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
      sb.Append("  ApprovedDate: ").Append(ApprovedDate).Append("\n");
      sb.Append("  State: ").Append(State).Append("\n");
      sb.Append("  ProposedSignature: ").Append(ProposedSignature).Append("\n");
      sb.Append("  ApprovedSignature: ").Append(ApprovedSignature).Append("\n");
      sb.Append("  CommittedSignature: ").Append(CommittedSignature).Append("\n");
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
