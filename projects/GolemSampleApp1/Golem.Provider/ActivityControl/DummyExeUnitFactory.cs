using System;
using System.Collections.Generic;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.ActivityControl
{
    /// <summary>
    /// Dummy ExeUnit Factory which only returns a ExeUnitCore on DummyExeUnitWorker, so effectively an emulator of an ExeUnit for testing purposes.
    /// </summary>
    public class DummyExeUnitFactory : IExeUnitFactory
    {
        public IExeUnit BuildExeUnit(Agreement agreement)
        {
            return new ExeUnitCore<DummyExeUnitWorker>();
        }
    }
}
