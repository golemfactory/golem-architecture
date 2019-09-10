using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Golem.Provider.Entities;

namespace Golem.Provider.ActivityControl
{
    public class DummyExeUnitWorker : IExeUnitWorker
    {
        public ExeUnitState State {
            get;
            protected set;
        }

        protected System.Timers.Timer UsageTimer;

        public string[] UsageVectorCounters { get; } = new string[] { "golem.usage.duration_sec" };

        public double[] UsageVector { get; } = new double[1];

        public event StateChanged OnStateChanged;

        public void Initialize(Agreement agreement)
        {
            this.State = ExeUnitState.Undefined;

            this.UsageTimer = new System.Timers.Timer()
            {
                Interval = 1000
            };

            this.UsageTimer.Elapsed += UsageTimer_Elapsed;

        }

        private void UsageTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // increment the usage counter by timer's interval
            this.UsageVector[0] += this.UsageTimer.Interval / 1000.0;
        }

        public CreateActivityResult CreateActivity(Activity activity)
        {
            lock (this)
            {
                if (this.State == ExeUnitState.Undefined)
                {
                    this.State = ExeUnitState.New;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });

                    return new CreateActivityResult()
                    {
                        Result = ExeUnitResultType.Ok
                    };
                }
                else
                {
                    return new CreateActivityResult()
                    {
                        Result = ExeUnitResultType.Error,
                        Message = "Activity already created..."
                    };

                }
            }
        }

        public ExeScriptCommandResult Deploy(ExeScriptCommand command, int commandIndex)
        {
            lock (this)
            {
                if (this.State == ExeUnitState.New)
                {

                    // Move to "deploying" state
                    this.State = ExeUnitState.Deploying;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });

                    // simulate deployment time
                    Thread.Sleep(5000);

                    // Move to "ready" state and return
                    this.State = ExeUnitState.Ready;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });

                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Ok
                    };
                }
                else
                {
                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Error,
                        Message = $"ExeUnit already in {this.State}, cannot deploy."
                    };

                }
            }
        }

        public ExeUnitResult DestroyActivity()
        {
            lock (this)
            {
                this.State = ExeUnitState.Terminated;

                this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                {
                    State = this.State
                });

                return new ExeUnitResult()
                {
                    Result = ExeUnitResultType.Ok
                };
            }
        }

        public ExeScriptCommandResult Run(ExeScriptCommand command, int commandIndex)
        {
            if(this.State == ExeUnitState.Active)
            {
                // this command does not change state, so only simulate time required and some response...
                Thread.Sleep(2000);

                return new ExeScriptCommandResult()
                {
                    Index = commandIndex,
                    Result = ExeUnitResultType.Ok,
                    Message = $"RUN {String.Join(" ", command?.Params)} returned: Success!"
                };

            }
            else
            {
                return new ExeScriptCommandResult()
                {
                    Index = commandIndex,
                    Result = ExeUnitResultType.Error,
                    Message = $"ExeUnit in {this.State}, cannot run command."
                };
            }
        }

        public ExeScriptCommandResult Start(ExeScriptCommand command, int commandIndex)
        {
            lock (this)
            {
                if (this.State == ExeUnitState.Ready)
                {

                    // Move to "starting" state
                    this.State = ExeUnitState.Starting;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });

                    // simulate start time
                    Thread.Sleep(2000);

                    // Move to "active" state and return
                    this.State = ExeUnitState.Active;

                    // start usage timer
                    this.UsageTimer.Enabled = true;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });

                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Ok
                    };
                }
                else
                {
                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Error,
                        Message = $"ExeUnit in {this.State}, cannot start."
                    };
                }
            }
        }

        public ExeScriptCommandResult Stop(ExeScriptCommand command, int commandIndex)
        {
            lock (this)
            {
                if (this.State == ExeUnitState.Active)
                {
                    // simulate stop time
                    Thread.Sleep(2000);

                    // Move to "terminated" state
                    this.State = ExeUnitState.Terminated;

                    // stop usage timer
                    this.UsageTimer.Enabled = false;

                    this.OnStateChanged?.Invoke(new ExeUnitStateDetails()
                    {
                        State = this.State
                    });
                    
                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Ok
                    };
                }
                else
                {
                    return new ExeScriptCommandResult()
                    {
                        Index = commandIndex,
                        Result = ExeUnitResultType.Error,
                        Message = $"ExeUnit in {this.State}, cannot start."
                    };
                }
            }
        }

        public ExeScriptCommandResult Transfer(ExeScriptCommand command, int commandIndex)
        {
            if (this.State == ExeUnitState.Active)
            {
                // simulate transfer time
                Thread.Sleep(7000);

                return new ExeScriptCommandResult()
                {
                    Index = commandIndex,
                    Result = ExeUnitResultType.Ok
                };
            }
            else
            {
                return new ExeScriptCommandResult()
                {
                    Index = commandIndex,
                    Result = ExeUnitResultType.Error,
                    Message = "ExeUnit not in active state."
                };

            }
        }
    }

}
