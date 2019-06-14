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
  public class AgreementProposal {

        /// <summary>
        /// Gets or Sets Demand
        /// </summary>
        [DataMember(Name = "demand", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "demand")]
        public Demand Demand { get; set; }

        /// <summary>
        /// Gets or Sets Constraints
        /// </summary>
        [DataMember(Name = "offer", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "offer")]
        public Offer Offer { get; set; }

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class AgreementProposal {\n");
            sb.Append("  Demand: ").Append(Demand).Append("\n");
            sb.Append("  Offer: ").Append(Offer).Append("\n");
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
