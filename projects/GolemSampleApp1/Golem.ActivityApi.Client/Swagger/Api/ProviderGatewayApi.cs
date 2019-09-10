using System;
using System.Collections.Generic;
using RestSharp;
using Golem.ActivityApi.Client.Swagger.Client;
using Golem.ActivityApi.Client.Swagger.Model;

namespace Golem.ActivityApi.Client.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IProviderGatewayApi
    {
        /// <summary>
        /// Queries for ExeScript batch results. 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>List&lt;ProviderEvent&gt;</returns>
        List<ProviderEvent> CollectActivityEvents (int? timeout);
        /// <summary>
        /// Pass activity state (which may include error details) 
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        void PutActivityStateDetails (string activityId, ActivityStateDetails state);
        /// <summary>
        /// Pass result of individual ExeScript command executed within the batch. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="commandResult"></param>
        /// <returns></returns>
        void PutExeScriptResult (string activityId, string batchId, ExeScriptCommandResult commandResult);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ProviderGatewayApi : IProviderGatewayApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderGatewayApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ProviderGatewayApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderGatewayApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProviderGatewayApi(String basePath)
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
        /// Queries for ExeScript batch results. 
        /// </summary>
        /// <param name="timeout"></param> 
        /// <returns>List&lt;ProviderEvent&gt;</returns>            
        public List<ProviderEvent> CollectActivityEvents (int? timeout)
        {
            
    
            var path = "/activity/events";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (timeout != null) queryParams.Add("timeout", ApiClient.ParameterToString(timeout)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectActivityEvents: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectActivityEvents: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ProviderEvent>) ApiClient.Deserialize(response.Content, typeof(List<ProviderEvent>), response.Headers);
        }
    
        /// <summary>
        /// Pass activity state (which may include error details) 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <param name="state"></param> 
        /// <returns></returns>            
        public void PutActivityStateDetails (string activityId, ActivityStateDetails state)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling PutActivityStateDetails");
            
    
            var path = "/activity/{activityId}/state";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(state); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling PutActivityStateDetails: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PutActivityStateDetails: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Pass result of individual ExeScript command executed within the batch. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <param name="batchId"></param> 
        /// <param name="commandResult"></param> 
        /// <returns></returns>            
        public void PutExeScriptResult (string activityId, string batchId, ExeScriptCommandResult commandResult)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling PutExeScriptResult");
            
            // verify the required parameter 'batchId' is set
            if (batchId == null) throw new ApiException(400, "Missing required parameter 'batchId' when calling PutExeScriptResult");
            
    
            var path = "/activity/{activityId}/exec/{batchId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
path = path.Replace("{" + "batchId" + "}", ApiClient.ParameterToString(batchId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(commandResult); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling PutExeScriptResult: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PutExeScriptResult: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
