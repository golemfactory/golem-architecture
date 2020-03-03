using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using activity_api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace activity_api.Controllers
{
    [Route("")]
    [ApiController]
    public class StateController : ControllerBase
    {
        /// <summary>
        /// Get state of specified Activity.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("activity/{activityId}/state")]
        [ProducesResponseType(typeof(ActivityState), 200)]
        [ProducesResponseType(typeof(ErrorBase), 404)] // Not found 
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error 
        public ActionResult<ActivityState> GetState(int activityId)
        {
            return new ActivityState()
            {
                State = ActivityStateEnum.Active
            };
        }

        /// <summary>
        /// Get usage of specified Activity.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("activity/{activityId}/usage")]
        [ProducesResponseType(typeof(List<decimal>), 200)]
        [ProducesResponseType(typeof(ErrorBase), 404)] // Not found 
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error 
        public ActionResult<List<decimal>> GetCurrentUsage(int activityId)
        {
            return new List<decimal>()
            {
                0.1m, 23.4m, 0.3m
            };
        }


        /// <summary>
        /// Get running command for a specified Activity.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("activity/{activityId}/command")]
        [ProducesResponseType(typeof(ExeScriptCommand), 200)]
        [ProducesResponseType(typeof(ErrorBase), 404)] // Not found 
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error 
        public ActionResult<ExeScriptCommand> GetRunningCommand(int activityId)
        {
            return new ExeScriptCommand()
            {
                
            };
        }


    }
}