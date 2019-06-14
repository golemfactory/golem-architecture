using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public class ExeScriptCommand
    {
        public string Command { get; set; }

        public List<string> Params { get; set; }
    }
}
