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
        /// <returns>List&lt;ProposalEvent&gt;</returns>
        List<ProposalEvent> CollectOffers (string subscriptionId, int? timeout, int? maxEvents);
        /// <summary>
        /// Sends Agreement draft to the Provider. Confirms Agreement self-created via &#x60;POST /agreements&#x60;
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void ConfirmAgreement (string agreementId);
        /// <summary>
        /// Creates Agreement from selected Proposal. Moves given Proposal to &#x60;Approved&#x60; state.
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        string CreateAgreement (AgreementProposal body);
        /// <summary>
        /// Responds with a bespoke Demand to received Offer. Creates and sends a modified version of original Demand (a counter-proposal) adjusted to previously received Proposal (ie. Offer). Changes Proposal state to &#x60;Draft&#x60;. Returns created Proposal id. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>string</returns>
        string CreateProposalDemand (Proposal body, string subscriptionId, string proposalId);
        /// <summary>
        /// Fetches agreement with given agreement id. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns>Agreement</returns>
        Agreement GetAgreement (string agreementId);
        /// <summary>
        /// Fetches Proposal (ie. Offer) with given proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>Proposal</returns>
        Proposal GetProposalOffer (string subscriptionId, string proposalId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <returns></returns>
        void PostQueryResponseDemands (PropertyQueryResponse body, string subscriptionId, string queryId);
        /// <summary>
        /// Rejects Proposal (ie. Offer) 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        void RejectProposalOffer (string subscriptionId, string proposalId);
        /// <summary>
        /// Publishes Demand 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        string SubscribeDemand (Demand body);
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
        void UnsubscribeDemand (string subscriptionId);
        /// <summary>
        /// Waits for Agreement approval by the Provider. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <param name="timeout"></param>
        /// <returns>string</returns>
        string WaitForApproval (string agreementId, int? timeout);
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
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
        /// <returns>List&lt;ProposalEvent&gt;</returns>
        public List<ProposalEvent> CollectOffers (string subscriptionId, int? timeout, int? maxEvents)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling CollectOffers");
    
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectOffers: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectOffers: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<ProposalEvent>) ApiClient.Deserialize(response.Content, typeof(List<ProposalEvent>), response.Headers);
        }
    
        /// <summary>
        /// Sends Agreement draft to the Provider. Confirms Agreement self-created via &#x60;POST /agreements&#x60;
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling ConfirmAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling ConfirmAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Creates Agreement from selected Proposal. Moves given Proposal to &#x60;Approved&#x60; state.
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        public string CreateAgreement (AgreementProposal body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling CreateAgreement");
    
            var path = "/agreements";
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateAgreement: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateAgreement: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
        /// <summary>
        /// Responds with a bespoke Demand to received Offer. Creates and sends a modified version of original Demand (a counter-proposal) adjusted to previously received Proposal (ie. Offer). Changes Proposal state to &#x60;Draft&#x60;. Returns created Proposal id. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>string</returns>
        public string CreateProposalDemand (Proposal body, string subscriptionId, string proposalId)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling CreateProposalDemand");
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling CreateProposalDemand");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling CreateProposalDemand");
    
            var path = "/demands/{subscriptionId}/proposals/{proposalId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposalDemand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CreateProposalDemand: " + response.ErrorMessage, response.ErrorMessage);
    
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
        /// Fetches Proposal (ie. Offer) with given proposal id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>Proposal</returns>
        public Proposal GetProposalOffer (string subscriptionId, string proposalId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling GetProposalOffer");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling GetProposalOffer");
    
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposalOffer: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling GetProposalOffer: " + response.ErrorMessage, response.ErrorMessage);
    
            return (Proposal) ApiClient.Deserialize(response.Content, typeof(Proposal), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <returns></returns>
        public void PostQueryResponseDemands (PropertyQueryResponse body, string subscriptionId, string queryId)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling PostQueryResponseDemands");
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling PostQueryResponseDemands");
            // verify the required parameter 'queryId' is set
            if (queryId == null) throw new ApiException(400, "Missing required parameter 'queryId' when calling PostQueryResponseDemands");
    
            var path = "/demands/{subscriptionId}/propertyQuery/{queryId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryResponseDemands: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryResponseDemands: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Rejects Proposal (ie. Offer) 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        public void RejectProposalOffer (string subscriptionId, string proposalId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling RejectProposalOffer");
            // verify the required parameter 'proposalId' is set
            if (proposalId == null) throw new ApiException(400, "Missing required parameter 'proposalId' when calling RejectProposalOffer");
    
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
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.DELETE, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposalOffer: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling RejectProposalOffer: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Publishes Demand 
        /// </summary>
        /// <param name="body"></param>
        /// <returns>string</returns>
        public string SubscribeDemand (Demand body)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling SubscribeDemand");
    
            var path = "/demands";
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
                throw new ApiException ((int)response.StatusCode, "Error calling SubscribeDemand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubscribeDemand: " + response.ErrorMessage, response.ErrorMessage);
    
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
        public void UnsubscribeDemand (string subscriptionId)
        {
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling UnsubscribeDemand");
    
            var path = "/demands/{subscriptionId}";
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
                throw new ApiException ((int)response.StatusCode, "Error calling UnsubscribeDemand: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UnsubscribeDemand: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Waits for Agreement approval by the Provider. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <param name="timeout"></param>
        /// <returns>string</returns>
        public string WaitForApproval (string agreementId, int? timeout)
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
    
             if (timeout != null) queryParams.Add("timeout", ApiClient.ParameterToString(timeout)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] { "ApiKeyAuth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling WaitForApproval: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling WaitForApproval: " + response.ErrorMessage, response.ErrorMessage);
    
            return (string) ApiClient.Deserialize(response.Content, typeof(string), response.Headers);
        }
    
    }
}
