using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Golem.ActivityApi.Client.Swagger.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class ProblemDetails : Dictionary<String, Object> {
    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or Sets Title
    /// </summary>
    [DataMember(Name="title", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public int? Status { get; set; }

    /// <summary>
    /// Gets or Sets Detail
    /// </summary>
    [DataMember(Name="detail", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "detail")]
    public string Detail { get; set; }

    /// <summary>
    /// Gets or Sets Instance
    /// </summary>
    [DataMember(Name="instance", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "instance")]
    public string Instance { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ProblemDetails {\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Title: ").Append(Title).Append("\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  Detail: ").Append(Detail).Append("\n");
      sb.Append("  Instance: ").Append(Instance).Append("\n");
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
