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
    public class ControlController : ControllerBase
    {
        /// <summary>
        /// Creates new Activity based on given Agreement.
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("activity")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(CreateActivityError), 400)] // Bad request (unknown agreement id)
        [ProducesResponseType(403)] // Forbidden (no rights to the agreement)
        [ProducesResponseType(404)] // Not found (nonexistent agreement)
        [ProducesResponseType(typeof(CreateActivityError), 500)] // Server error
        public ActionResult<string> CreateActivity([FromBody] string agreementId)
        {
            return "";
        }

        /// <summary>
        /// Executes an ExeScript batch within a given Activity.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="script"></param>
        /// <returns>ExeScript batch Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ExecError), 400)] // Bad request (invalid command)
        [ProducesResponseType(403)] // Forbidden (no rights to activity)
        [ProducesResponseType(404)] // Not found (nonexistent activity)
        [ProducesResponseType(typeof(ExecError), 500)] // Server error
        [Route("activity/{activityId}/exec")]
        public ActionResult<string> Exec(string activityId, ExeScriptRequest script)
        {
            return this.Ok("");
        }

        /// <summary>
        /// Queries for ExeScript batch results.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ExeScriptCommandResult>), 200)]
        [ProducesResponseType(typeof(ExecError), 400)]  // Bad request (invalid params)
        [ProducesResponseType(403)] // Forbidden (no rights to activity)
        [ProducesResponseType(404)] // Not found (nonexistent activity)
        [ProducesResponseType(typeof(ErrorBase), 500)] // Server error
        [Route("activity/{activityId}/exec/{batchId}")]
        public ActionResult<List<ExeScriptCommandResult>> GetExecBatchResults(string activityId, string batchId, [FromQuery] int timeout)
        {
            return null;
        }


        /// <summary>
        /// Destroys given Activity.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)] // Forbidden (no rights to activity)
        [ProducesResponseType(404)] // Not found (nonexistent activity)
        [ProducesResponseType(typeof(DestroyActivityError), 500)] // Server error
        [Route("activity/{activityId}")]
        public ActionResult DestroyActivity(string activityId)
        {
            return this.Ok();
        }


    }
}