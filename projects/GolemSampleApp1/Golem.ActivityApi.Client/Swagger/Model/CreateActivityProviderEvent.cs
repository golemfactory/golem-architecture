using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Golem.ActivityApi.Client.Swagger.Model
{

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CreateActivityProviderEvent : ProviderEvent {
    /// <summary>
    /// Gets or Sets AgreementId
    /// </summary>
    [DataMember(Name="agreementId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "agreementId")]
    public string AgreementId { get; set; }

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateActivityProviderEvent {\n");
      sb.Append("  AgreementId: ").Append(AgreementId).Append("\n");
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
