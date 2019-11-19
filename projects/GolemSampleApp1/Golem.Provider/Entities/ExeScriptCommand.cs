using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public class ExeScriptCommand
    {
        public string Command { get; set; }

        public List<string> Params { get; set; }

        public override string ToString()  {
        var sb = new StringBuilder();
        sb.Append("class ExeScriptCommand {");
        sb.Append(" Command: ").Append(Command);
        sb.Append(", Params: ").Append(Params);
        sb.Append("}");
        return sb.ToString();
        }

    }
}
