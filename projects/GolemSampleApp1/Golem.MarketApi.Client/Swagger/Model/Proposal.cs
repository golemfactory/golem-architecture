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
    public class Proposal : DemandOfferBase
    {
        /// <summary>
        /// Gets or Sets ProposalId
        /// </summary>
        [DataMember(Name = "proposalId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "proposalId")]
        public string ProposalId { get; set; }

        /// <summary>
        /// Gets or Sets IssuerId
        /// </summary>
        [DataMember(Name = "issuerId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "issuerId")]
        public string IssuerId { get; set; }

        /// <summary>
        /// * `Initial` - proposal arrived from the market as response to subscription * `Draft` - bespoke counter-proposal issued by one party directly to other party (negotiation phase) * `Rejected` by other party * `Accepted` - promoted into the Agreement draft * `Expired` - not accepted nor rejected before validity period 
        /// </summary>
        /// <value>* `Initial` - proposal arrived from the market as response to subscription * `Draft` - bespoke counter-proposal issued by one party directly to other party (negotiation phase) * `Rejected` by other party * `Accepted` - promoted into the Agreement draft * `Expired` - not accepted nor rejected before validity period </value>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// id of the proposal from other side which this proposal responds to 
        /// </summary>
        /// <value>id of the proposal from other side which this proposal responds to </value>
        [DataMember(Name = "prevProposalId", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "prevProposalId")]
        public string PrevProposalId { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Proposal {\n");
            sb.Append("  ProposalId: ").Append(ProposalId).Append("\n");
            sb.Append("  IssuerId: ").Append(IssuerId).Append("\n");
            sb.Append("  Properties: ").Append(Properties).Append("\n");
            sb.Append("  Constraints: ").Append(Constraints).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  PrevProposalId: ").Append(PrevProposalId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
