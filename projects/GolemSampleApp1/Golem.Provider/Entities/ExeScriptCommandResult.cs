using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public class ExeScriptCommandResult : ExeUnitResult
    {
        /// <summary>
        /// Sequence number of ExeScript command in the batch 
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Flag marking the final command result from a batch, either because it is a last command, or its result is an Error.
        /// </summary>
        public bool IsBatchFinished { get; set; }
    }
}
