using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.ActivityControl
{
    public interface IExeUnitFactory
    {
        /// <summary>
        /// Builds a new ExeUnit for a given Agreement.
        /// The factory expects the Agreement's Offer properties to contain all the properties required to instantiate an ExeUnit.
        /// </summary>
        /// <param name="agreement"></param>
        /// <returns></returns>
        IExeUnit BuildExeUnit(Agreement agreement);
    }
}
