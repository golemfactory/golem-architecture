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
  public class ExeScriptCommand {
    /// <summary>
    /// Gets or Sets Command
    /// </summary>
    [DataMember(Name="command", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "command")]
    public string Command { get; set; }

    /// <summary>
    /// Gets or Sets Params
    /// </summary>
    [DataMember(Name="params", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "params")]
    public List<string> Params { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class ExeScriptCommand {\n");
      sb.Append("  Command: ").Append(Command).Append("\n");
      sb.Append("  Params: ").Append(Params).Append("\n");
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
