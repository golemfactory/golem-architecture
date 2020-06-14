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
    public class ProviderGatewayController : ControllerBase
    {

        /// <summary>
        /// Queries for ExeScript batch results.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProviderEvent>), 200)]
        [ProducesResponseType(403)] // Forbidden 
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error
        [Route("activity/events")]
        public ActionResult<List<ProviderEvent>> CollectActivityEvents([FromQuery] int timeout)
        {
            return null;
        }

        /// <summary>
        /// Pass activity state (which may include error details)
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="commandResult"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)] // Forbidden (no rights to activity)
        [ProducesResponseType(404)] // Not found (nonexistent activity)
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error
        [Route("activity/{activityId}/state")]
        public ActionResult PutActivityStateDetails([FromRoute] string activityId, ActivityStateDetails state)
        {
            return null;
        }


        /// <summary>
        /// Pass result of individual ExeScript command executed within the batch.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="commandResult"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)] // Forbidden (no rights to activity)
        [ProducesResponseType(404)] // Not found (nonexistent activity)
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error
        [Route("activity/{activityId}/exec/{batchId}")]
        public ActionResult PutExeScriptResult([FromRoute] string activityId, [FromRoute] string batchId, ExeScriptCommandResult commandResult)
        {
            return null;
        }

    }
}