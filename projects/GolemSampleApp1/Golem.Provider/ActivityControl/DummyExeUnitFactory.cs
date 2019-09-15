using System;
using System.Collections.Generic;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.ActivityControl
{
    public class DummyExeUnitFactory : IExeUnitFactory
    {
        public IExeUnit BuildExeUnit(Agreement agreement)
        {
            throw new NotImplementedException();
        }
    }
}
