using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.ActivityControl
{
    public delegate void StateChanged(ExeUnitStateDetails newStateDetails);

    public interface IExeUnit
    {
        
        /// <summary>
        /// Gets called when Activity is created.
        /// </summary>
        /// <param name="activity"></param>
        CreateActivityResult CreateActivity(Activity activity);

        /// <summary>
        /// Execute an ExeScript command.
        /// Note this call may effectively alter the state of Activity which is related to this ExeUnit.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ExeScriptCommandResult ExecCommand(ExeScriptCommand command);

        /// <summary>
        /// Get the State details of the ExeUnit, including:
        /// - current usage vector value
        /// </summary>
        /// <returns></returns>
        ExeUnitStateDetails GetStateDetails();

        void Destroy();

        /// <summary>
        /// Event declaration - way to notify an observer about ExeUnit state change
        /// </summary>
        event StateChanged OnStateChanged;

    }
}
