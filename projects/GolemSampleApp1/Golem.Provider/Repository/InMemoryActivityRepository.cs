using System;
using System.Collections.Generic;
using System.Text;
using Golem.Provider.Entities;

namespace Golem.Provider.Repository
{
    public class InMemoryActivityRepository : IActivityRepository
    {
        protected Dictionary<string, Activity> ActivitiesByActivityId { get; set; } = new Dictionary<string, Activity>();

        public Activity CreateActivity(Activity activity)
        {
            lock (this.ActivitiesByActivityId)
            {
                if (this.ActivitiesByActivityId.ContainsKey(activity.Id))
                {
                    throw new Exception($"Activity Id {activity.Id} already exists.");
                }

                this.ActivitiesByActivityId.Add(activity.Id, activity);
            }

            return activity;
        }

        public void SetActivityState(string activityId, ActivityState state)
        {
            if (this.ActivitiesByActivityId.ContainsKey(activityId))
            {
                var activity = this.ActivitiesByActivityId[activityId];

                lock (activity)
                {
                    activity.SetState(state);
                }
            }
            else
            {
                throw new Exception($"Activity Id {activityId} does not exist.");
            }

        }
    }
}
