using System;
using System.Collections.Generic;
using RestSharp;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
using Golem.MarketApi.Client.Swagger.Model.Converters;

namespace Golem.MarketApi.Client.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRequestorApi
    {
        /// <summary>
        /// Cancels agreement. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void CancelAgreement (string agreementId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="timeout"></param>
        /// <param name="maxEvents"></param>
        /// <returns>List&lt;RequestorEvent&gt;</returns>
        List<RequestorEvent> Collect (string subscriptionId, float? timeout, long? maxEvents);
        /// <summary>
        /// approves 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void ConfirmAgreement (string agreementId);
        /// <summary>
        /// Creates new agreement from proposal 
        /// </summary>
        /// <param name="agreement"></param>
        /// <returns></returns>
        void CreateAgreement (Agreement agreement);
        /// <summary>
        /// Creates agreement proposal 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <param name="demandProposal"></param>
        /// <returns>string</returns>
        string CreateProposal (string subscriptionId, string proposalId, Proposal demandProposal);
        /// <summary>
        /// Fetches agreement proposal from proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>AgreementProposal</returns>
        AgreementProposal GetProposal (string subscriptionId, string proposalId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        void QueryResponse (string subscriptionId, string queryId, PropertyQueryResponse propertyValues);
        /// <summary>
        /// Rejects offer 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        void RejectProposal (string subscriptionId, string proposalId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body">Demand description</param>
        /// <returns>string</returns>
        string Subscribe (Demand body);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        void Unsubscribe (string subscriptionId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void WaitForApproval (string agreementId);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class RequestorApi : IRequestorApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestorApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public RequestorApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestorApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RequestorApi(String basePath)
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
        /// Cancels agreement. 
        /// </summary>
        /// <param name="agreementId"></param> 
        /// <returns></returns>            
        public void CancelAgreement (string agreementId)
        {
            
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling CancelAgreement");
            
    
            var path = "/agreements/{agreementId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling CancelAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CancelAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <param name="timeout"></param> 
        /// <param name="maxEvents"></param> 
        /// <returns>List&lt;RequestorEvent&gt;</returns>            
        public List<RequestorEvent> Collect (string subscriptionId, float? timeout, long? maxEvents)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling Collect");
            
    
            var path = "/demands/{subscriptionId}/events";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (timeout != null) queryParams.Add("timeout", ApiClient.ParameterToString(timeout)); // query parameter
 if (maxEvents != null) queryParams.Add("maxEvents", ApiClient.ParameterToString(maxEvents)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling Collect: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling Collect: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<RequestorEvent>) ApiClient.Deserialize(response.Content, typeof(List<RequestorEvent>), response.Headers, new RequestorEventConverter());
        }
    
        /// <summary>
        /// approves 
        /// </summary>
        /// <param name="agreementId"></param> 
        /// <returns></returns>            
        public void ConfirmAgreement (string agreementId)
        {
            
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling ConfirmAgreement");
            
    
            var path = "/agreements/{agreementId}/confirm";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ConfirmAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ConfirmAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Creates new agreement from proposal 
        /// </summary>
        /// <param name="agreement"></param> 
        /// <returns></returns>            
        public void CreateAgreement (Agreement agreement)
        {
            
            // verify the required parameter 'agreement' is set
            if (agreement == null) throw new ApiException(400, "Missing required parameter 'agreement' when calling CreateAgreement");
            
    
            var path = "/agreements";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(agreement); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Creates agreement proposal 
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <param name="proposalId"></param> 
        /// <param name="demandProposal"></param> 
        /// <returns>string</returns>            
        public string CreateProposal (string subscriptionId, string proposalId, Proposal demandProposal)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling CreateProposal");
            
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling CreateProposal");
            
    
            var path = "/demands/{subscriptionId}/proposals/{proposalId}/demand";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(demandProposal); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposal: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposal: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Fetches agreement proposal from proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <param name="proposalId"></param> 
        /// <returns>AgreementProposal</returns>            
        public AgreementProposal GetProposal (string subscriptionId, string proposalId)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling GetProposal");
            
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling GetProposal");
            
    
            var path = "/demands/{subscriptionId}/proposals/{proposalId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposal: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposal: " + response.ErrorMessage, response.ErrorMessage);
    
            return (AgreementProposal) ApiClient.Deserialize(response.Content, typeof(AgreementProposal), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <param name="queryId"></param> 
        /// <param name="propertyValues"></param> 
        /// <returns></returns>            
        public void QueryResponse (string subscriptionId, string queryId, PropertyQueryResponse propertyValues)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling QueryResponse");
            
            // verify the required parameter 'queryId' is set
            if (queryId == null) throw new ApiException(400, "Missing required parameter 'queryId' when calling QueryResponse");
            
    
            var path = "/demands/{subscriptionId}/propertyQuery/{queryId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "queryId" + "}", ApiClient.ParameterToString(queryId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(propertyValues); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling QueryResponse: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling QueryResponse: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Rejects offer 
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <param name="proposalId"></param> 
        /// <returns></returns>            
        public void RejectProposal (string subscriptionId, string proposalId)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling RejectProposal");
            
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling RejectProposal");
            
    
            var path = "/demands/{subscriptionId}/proposals/{proposalId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposal: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposal: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body">Demand description</param> 
        /// <returns>string</returns>            
        public string Subscribe (Demand body)
        {
            
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling Subscribe");
            
    
            var path = "/demands";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(body); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling Subscribe: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling Subscribe: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param> 
        /// <returns></returns>            
        public void Unsubscribe (string subscriptionId)
        {
            
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling Unsubscribe");
            
    
            var path = "/demands/{subscriptionId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
    
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
                throw new ApiException ((int)response.StatusCode, "Error calling Unsubscribe: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling Unsubscribe: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="agreementId"></param> 
        /// <returns></returns>            
        public void WaitForApproval (string agreementId)
        {
            
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling WaitForApproval");
            
    
            var path = "/agreements/{agreementId}/wait";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling WaitForApproval: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling WaitForApproval: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
