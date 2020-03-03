using System;
using System.Collections.Generic;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.ActivityControl
{
    public class HostDirectExeUnitWorker : IExeUnitWorker
    {
        public ExeUnitState State => throw new NotImplementedException();

        public string[] UsageVectorCounters => throw new NotImplementedException();

        public double[] UsageVector => throw new NotImplementedException();

        public event StateChanged OnStateChanged;

        public CreateActivityResult CreateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public ExeScriptCommandResult Deploy(ExeScriptCommand command, int commandIndex)
        {
            throw new NotImplementedException();
        }

        public ExeUnitResult DestroyActivity()
        {
            throw new NotImplementedException();
        }

        public void Initialize(Agreement agreement)
        {
            // Extract the exe name from Agreement
            // TODO consider available sources of the exe for download...

            // Extract the working directory path from configuration
            // Re TRANSFER - all local paths are relative to workingdirectory for this ExeUnitWorker

        }

        public ExeScriptCommandResult Run(ExeScriptCommand command, int commandIndex)
        {
            throw new NotImplementedException();
        }

        public ExeScriptCommandResult Start(ExeScriptCommand command, int commandIndex)
        {
            throw new NotImplementedException();
        }

        public ExeScriptCommandResult Stop(ExeScriptCommand command, int commandIndex)
        {
            throw new NotImplementedException();
        }

        public ExeScriptCommandResult Transfer(ExeScriptCommand command, int commandIndex)
        {
            throw new NotImplementedException();
        }
    }

}
