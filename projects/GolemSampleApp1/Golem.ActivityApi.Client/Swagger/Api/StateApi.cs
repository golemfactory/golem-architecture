using System;
using System.Collections.Generic;
using RestSharp;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;

namespace Golem.MarketApi.Client.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IStateApi
    {
        /// <summary>
        /// Get usage of specified Activity. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>List&lt;double?&gt;</returns>
        List<double?> GetCurrentUsage (int? activityId);
        /// <summary>
        /// Get running command for a specified Activity. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>ExeScriptCommand</returns>
        ExeScriptCommand GetRunningCommand (int? activityId);
        /// <summary>
        /// Get state of specified Activity. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>ActivityState</returns>
        ActivityState GetState (int? activityId);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class StateApi : IStateApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public StateApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="StateApi"/> class.
        /// </summary>
        /// <returns></returns>
        public StateApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        /// Get usage of specified Activity. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <returns>List&lt;double?&gt;</returns>            
        public List<double?> GetCurrentUsage (int? activityId)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling GetCurrentUsage");
            
    
            var path = "/activity/{activityId}/usage";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetCurrentUsage: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetCurrentUsage: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<double?>) ApiClient.Deserialize(response.Content, typeof(List<double?>), response.Headers);
        }
    
        /// <summary>
        /// Get running command for a specified Activity. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <returns>ExeScriptCommand</returns>            
        public ExeScriptCommand GetRunningCommand (int? activityId)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling GetRunningCommand");
            
    
            var path = "/activity/{activityId}/command";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRunningCommand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetRunningCommand: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ExeScriptCommand) ApiClient.Deserialize(response.Content, typeof(ExeScriptCommand), response.Headers);
        }
    
        /// <summary>
        /// Get state of specified Activity. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <returns>ActivityState</returns>            
        public ActivityState GetState (int? activityId)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling GetState");
            
    
            var path = "/activity/{activityId}/state";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetState: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetState: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ActivityState) ApiClient.Deserialize(response.Content, typeof(ActivityState), response.Headers);
        }
    
    }
}
