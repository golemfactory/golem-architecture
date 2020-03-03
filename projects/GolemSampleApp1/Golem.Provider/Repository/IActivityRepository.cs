using Golem.Provider.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golem.Provider.Repository
{
    public interface IActivityRepository
    {
        Activity CreateActivity(Activity activity);
        void SetActivityState(string activityId, ActivityState state);
    }
}
