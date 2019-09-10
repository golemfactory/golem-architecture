using System;
using System.Collections.Generic;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IControlApi
    {
        /// <summary>
        /// Creates new Activity based on given Agreement. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>string</returns>
        string CreateActivity (string agreementId);
        /// <summary>
        /// Destroys given Activity. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        void DestroyActivity (string activityId);
        /// <summary>
        /// Executes an ExeScript batch within a given Activity. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="script"></param>
        /// <returns>string</returns>
        string Exec (string activityId, ExeScriptBatch script);
        /// <summary>
        /// Queries for ExeScript batch results. 
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="batchId"></param>
        /// <param name="timeout"></param>
        /// <returns>List&lt;ExeScriptCommandResult&gt;</returns>
        List<ExeScriptCommandResult> GetExecBatchResults (string activityId, int? batchId, int? timeout);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ControlApi : IControlApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ControlApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ControlApi(String basePath)
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
        /// Creates new Activity based on given Agreement. 
        /// </summary>
        /// <param name="agreementId"></param> 
        /// <returns>string</returns>            
        public string CreateActivity (string agreementId)
        {
            
    
            var path = "/activity";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(agreementId); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Destroys given Activity. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <returns></returns>            
        public void DestroyActivity (string activityId)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling DestroyActivity");
            
    
            var path = "/activity/{activityId}";
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
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling DestroyActivity: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling DestroyActivity: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Executes an ExeScript batch within a given Activity. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <param name="script"></param> 
        /// <returns>string</returns>            
        public string Exec (string activityId, ExeScriptBatch script)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling Exec");
            
    
            var path = "/activity/{activityId}/exec";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(script); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling Exec: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling Exec: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Queries for ExeScript batch results. 
        /// </summary>
        /// <param name="activityId"></param> 
        /// <param name="batchId"></param> 
        /// <param name="timeout"></param> 
        /// <returns>List&lt;ExeScriptCommandResult&gt;</returns>            
        public List<ExeScriptCommandResult> GetExecBatchResults (string activityId, int? batchId, int? timeout)
        {
            
            // verify the required parameter 'activityId' is set
            if (activityId == null) throw new ApiException(400, "Missing required parameter 'activityId' when calling GetExecBatchResults");
            
            // verify the required parameter 'batchId' is set
            if (batchId == null) throw new ApiException(400, "Missing required parameter 'batchId' when calling GetExecBatchResults");
            
    
            var path = "/activity/{activityId}/exec/{batchId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "activityId" + "}", ApiClient.ParameterToString(activityId));
path = path.Replace("{" + "batchId" + "}", ApiClient.ParameterToString(batchId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetExecBatchResults: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetExecBatchResults: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ExeScriptCommandResult>) ApiClient.Deserialize(response.Content, typeof(List<ExeScriptCommandResult>), response.Headers);
        }
    
    }
}
