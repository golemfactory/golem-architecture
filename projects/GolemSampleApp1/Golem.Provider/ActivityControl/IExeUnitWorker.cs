using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.ActivityControl
{
    public interface IExeUnitWorker
    {
        /// <summary>
        /// Current state of the ExeUnit worker
        /// </summary>
        ExeUnitState State { get; }

        /// <summary>
        /// List of Golem usage counters which indicate the amount of resources being "burned" by the worker.
        /// </summary>
        string[] UsageVectorCounters { get; }

        /// <summary>
        /// Current value of usage vector. 
        /// Contains list of coefficients, and the sequence matches the sequence of counters indicated by UsageVecotrCounters property.
        /// </summary>
        double[] UsageVector { get; }


        void Initialize(Agreement agreement);

        CreateActivityResult CreateActivity(Activity activity);

        ExeScriptCommandResult Deploy(ExeScriptCommand command, int commandIndex);

        ExeScriptCommandResult Start(ExeScriptCommand command, int commandIndex);

        ExeScriptCommandResult Run(ExeScriptCommand command, int commandIndex);

        ExeScriptCommandResult Stop(ExeScriptCommand command, int commandIndex);

        ExeScriptCommandResult Transfer(ExeScriptCommand command, int commandIndex);

        ExeUnitResult DestroyActivity();

        /// <summary>
        /// Event declaration - way to notify an observer about ExeUnit state change
        /// </summary>
        event StateChanged OnStateChanged;


    }
}
