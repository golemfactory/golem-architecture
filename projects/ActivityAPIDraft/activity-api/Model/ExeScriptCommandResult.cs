using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Model
{
    public enum ExeScriptResultEnum
    {
        Ok,
        Error
    }

    public class ExeScriptCommandResult
    {
        public int Index { get; set; }

        public ExeScriptResultEnum Result { get; set; }

        public string Message { get; set; }
    }
}
