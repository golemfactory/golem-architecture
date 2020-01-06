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
    public interface IProviderApi
    {
        /// <summary>
        /// Approves Agreement proposed by the Reqestor. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void ApproveAgreement (string agreementId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="timeout"></param>
        /// <param name="maxEvents"></param>
        /// <returns>List&lt;&gt;</returns>
        List<> CollectDemands (string subscriptionId, int? timeout, int? maxEvents);
        /// <summary>
        /// Responds with a bespoke Offer to received Demand. Creates and sends a modified version of original Offer (a counter-proposal) adjusted to previously received Proposal (ie. Demand). Changes Proposal state to &#x60;Draft&#x60;. Returns created Proposal id. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>string</returns>
        string CreateProposalOffer (Proposal body, string subscriptionId, string proposalId);
        /// <summary>
        /// Fetches agreement with given agreement id. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>Agreement</returns>
        Agreement GetAgreement (string agreementId);
        /// <summary>
        /// Fetches Proposal (ie. Demand) with given proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>Proposal</returns>
        Proposal GetProposalDemand (string subscriptionId, string proposalId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <returns></returns>
        void PostQueryResponseOffers (PropertyQueryResponse body, string subscriptionId, string queryId);
        /// <summary>
        /// Rejects Agreement proposed by the Requestor. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void RejectAgreement (string agreementId);
        /// <summary>
        /// Rejects Proposal (ie. Demand) 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        void RejectProposalDemand (string subscriptionId, string proposalId);
        /// <summary>
        /// Publishes Offer 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        string SubscribeOffer (Offer body);
        /// <summary>
        /// Terminates approved Agreement. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void TerminateAgreement (string agreementId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        void UnsubscribeOffer (string subscriptionId);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class ProviderApi : IProviderApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public ProviderApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProviderApi(String basePath)
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
        /// Approves Agreement proposed by the Reqestor. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public void ApproveAgreement (string agreementId)
        {
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling ApproveAgreement");
    
            var path = "/agreements/{agreementId}/approve";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ApproveAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ApproveAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="timeout"></param>
        /// <param name="maxEvents"></param>
        /// <returns>List&lt;&gt;</returns>
        public List<> CollectDemands (string subscriptionId, int? timeout, int? maxEvents)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling CollectDemands");
    
            var path = "/offers/{subscriptionId}/events";
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectDemands: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectDemands: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<>) ApiClient.Deserialize(response.Content, typeof(List<>), response.Headers);
        }
    
        /// <summary>
        /// Responds with a bespoke Offer to received Demand. Creates and sends a modified version of original Offer (a counter-proposal) adjusted to previously received Proposal (ie. Demand). Changes Proposal state to &#x60;Draft&#x60;. Returns created Proposal id. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>string</returns>
        public string CreateProposalOffer (Proposal body, string subscriptionId, string proposalId)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling CreateProposalOffer");
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling CreateProposalOffer");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling CreateProposalOffer");
    
            var path = "/offers/{subscriptionId}/proposals/{proposalId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(body); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposalOffer: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposalOffer: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Fetches agreement with given agreement id. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>Agreement</returns>
        public Agreement GetAgreement (string agreementId)
        {
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling GetAgreement");
    
            var path = "/agreements/{agreementId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Agreement) ApiClient.Deserialize(response.Content, typeof(Agreement), response.Headers);
        }
    
        /// <summary>
        /// Fetches Proposal (ie. Demand) with given proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>Proposal</returns>
        public Proposal GetProposalDemand (string subscriptionId, string proposalId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling GetProposalDemand");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling GetProposalDemand");
    
            var path = "/offers/{subscriptionId}/proposals/{proposalId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposalDemand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposalDemand: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Proposal) ApiClient.Deserialize(response.Content, typeof(Proposal), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <returns></returns>
        public void PostQueryResponseOffers (PropertyQueryResponse body, string subscriptionId, string queryId)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling PostQueryResponseOffers");
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling PostQueryResponseOffers");
            // verify the required parameter 'queryId' is set
            if (queryId == null) throw new ApiException(400, "Missing required parameter 'queryId' when calling PostQueryResponseOffers");
    
            var path = "/offers/{subscriptionId}/propertyQuery/{queryId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "queryId" + "}", ApiClient.ParameterToString(queryId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(body); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryResponseOffers: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryResponseOffers: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Rejects Agreement proposed by the Requestor. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public void RejectAgreement (string agreementId)
        {
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling RejectAgreement");
    
            var path = "/agreements/{agreementId}/reject";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Rejects Proposal (ie. Demand) 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        public void RejectProposalDemand (string subscriptionId, string proposalId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling RejectProposalDemand");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling RejectProposalDemand");
    
            var path = "/offers/{subscriptionId}/proposals/{proposalId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
path = path.Replace("{" + "proposalId" + "}", ApiClient.ParameterToString(proposalId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposalDemand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposalDemand: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Publishes Offer 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        public string SubscribeOffer (Offer body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling SubscribeOffer");
    
            var path = "/offers";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(body); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubscribeOffer: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubscribeOffer: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Terminates approved Agreement. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public void TerminateAgreement (string agreementId)
        {
            // verify the required parameter 'agreementId' is set
            if (agreementId == null) throw new ApiException(400, "Missing required parameter 'agreementId' when calling TerminateAgreement");
    
            var path = "/agreements/{agreementId}/terminate";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "agreementId" + "}", ApiClient.ParameterToString(agreementId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling TerminateAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling TerminateAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public void UnsubscribeOffer (string subscriptionId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling UnsubscribeOffer");
    
            var path = "/offers/{subscriptionId}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subscriptionId" + "}", ApiClient.ParameterToString(subscriptionId));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UnsubscribeOffer: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UnsubscribeOffer: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
    }
}
