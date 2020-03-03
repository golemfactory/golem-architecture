using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.ActivityControl
{
    public delegate void StateChanged(ExeUnitStateDetails newStateDetails);

    public enum ExeUnitCommand
    {
        Unknown,
        Deploy,
        Start,
        Run,
        Stop,
        Transfer
    }

    public interface IExeUnit
    {


        /// <summary>
        /// Activity related to this ExeUnit
        /// </summary>
        Activity Activity { get; set; }

        /// <summary>
        /// Gets called when Activity is created.
        /// NOTE: The Activity parameter is expected to point to Agreement which includes all information required to initialize the ExeUnit, and to create activity later.
        /// This means that validation needs to happen here.
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
