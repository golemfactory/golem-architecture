using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Entities
{
    public enum ExeUnitResultType
    {
        Ok = 0,
        Error = 1
    }

    public class ExeUnitResult
    {
        /// <summary>
        /// Result code.
        /// </summary>
        public ExeUnitResultType Result { get; set; }

        /// <summary>
        /// String message included in the result.
        /// 
        /// </summary>
        public string Message { get; set; }


    }
}
