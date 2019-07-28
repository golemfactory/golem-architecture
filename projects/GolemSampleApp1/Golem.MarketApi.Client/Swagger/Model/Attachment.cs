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
  public class Attachment {
    /// <summary>
    /// Attachment name
    /// </summary>
    /// <value>Attachment name</value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Attachment mime type
    /// </summary>
    /// <value>Attachment mime type</value>
    [DataMember(Name="contentType", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "contentType")]
    public string ContentType { get; set; }

    /// <summary>
    /// hex encoded sha256 of attachment binary content
    /// </summary>
    /// <value>hex encoded sha256 of attachment binary content</value>
    [DataMember(Name="sha256", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "sha256")]
    public string Sha256 { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Attachment {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  ContentType: ").Append(ContentType).Append("\n");
      sb.Append("  Sha256: ").Append(Sha256).Append("\n");
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
