using System;
using System.Collections.Generic;
using RestSharp;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
using Newtonsoft.Json;
//using Golem.MarketApi.Client.Swagger.Model.Converters;

namespace Golem.MarketApi.Client.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRequestorApi
    {
        /// <summary>
        /// Cancels agreement. Causes the awaiting &#x60;waitForApproval&#x60; call to return with &#x60;Cancelled&#x60; response. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void CancelAgreement (string agreementId);
        /// <summary>
        /// Reads Market responses to published Demand. This is a blocking operation. It will not return until there is at least one new event.  **Note**: When &#x60;collectOffers&#x60; is waiting, simultaneous call to &#x60;unsubscribeDemand&#x60; on the same &#x60;subscriptionId&#x60; should result in \&quot;Subscription does not exist\&quot; error returned from &#x60;collectOffers&#x60;.  **Note**: Specification requires this endpoint to support list of specific Proposal Ids to listen for messages related only to specific Proposals. This is not covered yet. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="timeout"></param>
        /// <param name="maxEvents"></param>
        /// <returns>List&lt;ProposalEvent&gt;</returns>
        List<ProposalEvent> CollectOffers (string subscriptionId, int? timeout, int? maxEvents);
        /// <summary>
        /// Sends Agreement draft to the Provider. Signs Agreement self-created via &#x60;createAgreement&#x60; and sends it to the Provider. 
        /// </summary>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        void ConfirmAgreement (string agreementId);
        /// <summary>
        /// Creates Agreement from selected Proposal. Initiates the Agreement handshake phase.  Formulates an Agreement artifact from the Proposal indicated by the received Proposal Id.  The Approval Expiry Date is added to Agreement artifact and implies the effective timeout on the whole Agreement Confirmation sequence.  A successful call to &#x60;createAgreement&#x60; shall immediately be followed by a &#x60;confirmAgreement&#x60; and &#x60;waitForApproval&#x60; call in order to listen for responses from the Provider.  **Note**: Moves given Proposal to &#x60;Approved&#x60; state. 
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
        /// Fetches Proposal (Offer) with given id. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns>Proposal</returns>
        Proposal GetProposalOffer (string subscriptionId, string proposalId);
        /// <summary>
        /// Handles dynamic property query. The Market Matching Mechanism, when resolving the match relation for the specific Demand-Offer pair, is to detect the “dynamic” properties required (via constraints) by the other side. At this point, it is able to query the issuing node for those properties and submit the other side’s requested properties as the context of the query.  **Note**: The property query responses may be submitted in “chunks”, ie. the responder may choose to resolve ‘quick’/lightweight’ properties faster and provide response sooner, while still working on more time-consuming properties in the background. Therefore the response contains both the resolved properties, as well as list of properties which responder knows still require resolution.  **Note**: This method must be implemented for Market API Capability Level 2. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="queryId"></param>
        /// <returns></returns>
        void PostQueryReplyDemands (PropertyQueryReply body, string subscriptionId, string queryId);
        /// <summary>
        /// Rejects Proposal (Offer). Effectively ends a Negotiation chain - it explicitly indicates that the sender will not create another counter-Proposal. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        void RejectProposalOffer (string subscriptionId, string proposalId);
        /// <summary>
        /// Publishes Requestor capabilities via Demand. Demand object can be considered an \&quot;open\&quot; or public Demand, as it is not directed at a specific Provider, but rather is sent to the market so that the matching mechanism implementation can associate relevant Offers.  **Note**: it is an \&quot;atomic\&quot; operation, ie. as soon as Subscription is placed, the Demand is published on the market. 
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
        /// Stop subscription for previously published Demand. Stop receiving Proposals.  **Note**: this will terminate all pending &#x60;collectOffers&#x60; calls on this subscription. This implies, that client code should not &#x60;unsubscribeDemand&#x60; before it has received all expected/useful inputs from &#x60;collectOffers&#x60;. 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        void UnsubscribeDemand (string subscriptionId);
        /// <summary>
        /// Waits for Agreement approval by the Provider. This is a blocking operation. The call may be aborted by Requestor caller code. After the call is aborted, another &#x60;waitForApproval&#x60; call can be raised on the same Agreement Id.  It returns one of the following options: * &#x60;Ok&#x60; - Indicates that the Agreement has been approved by the Provider.   - The Provider is now ready to accept a request to start an Activity     as described in the negotiated agreement.   - The Requestor’s corresponding &#x60;waitForApproval&#x60; call returns Ok after     this on the Provider side.  * &#x60;Rejected&#x60; - Indicates that the Provider has called &#x60;rejectAgreement&#x60;,   which effectively stops the Agreement handshake. The Requestor may attempt   to return to the Negotiation phase by sending a new Proposal.  * &#x60;Cancelled&#x60; - Indicates that the Requestor himself has called  &#x60;cancelAgreement&#x60;, which effectively stops the Agreement handshake. 
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
        /// Cancels agreement. Causes the awaiting &#x60;waitForApproval&#x60; call to return with &#x60;Cancelled&#x60; response. 
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
        /// Reads Market responses to published Demand. This is a blocking operation. It will not return until there is at least one new event.  **Note**: When &#x60;collectOffers&#x60; is waiting, simultaneous call to &#x60;unsubscribeDemand&#x60; on the same &#x60;subscriptionId&#x60; should result in \&quot;Subscription does not exist\&quot; error returned from &#x60;collectOffers&#x60;.  **Note**: Specification requires this endpoint to support list of specific Proposal Ids to listen for messages related only to specific Proposals. This is not covered yet. 
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
        /// Sends Agreement draft to the Provider. Signs Agreement self-created via &#x60;createAgreement&#x60; and sends it to the Provider. 
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
        /// Creates Agreement from selected Proposal. Initiates the Agreement handshake phase.  Formulates an Agreement artifact from the Proposal indicated by the received Proposal Id.  The Approval Expiry Date is added to Agreement artifact and implies the effective timeout on the whole Agreement Confirmation sequence.  A successful call to &#x60;createAgreement&#x60; shall immediately be followed by a &#x60;confirmAgreement&#x60; and &#x60;waitForApproval&#x60; call in order to listen for responses from the Provider.  **Note**: Moves given Proposal to &#x60;Approved&#x60; state. 
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
        /// Fetches Proposal (Offer) with given id. 
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
        /// Handles dynamic property query. The Market Matching Mechanism, when resolving the match relation for the specific Demand-Offer pair, is to detect the “dynamic” properties required (via constraints) by the other side. At this point, it is able to query the issuing node for those properties and submit the other side’s requested properties as the context of the query.  **Note**: The property query responses may be submitted in “chunks”, ie. the responder may choose to resolve ‘quick’/lightweight’ properties faster and provide response sooner, while still working on more time-consuming properties in the background. Therefore the response contains both the resolved properties, as well as list of properties which responder knows still require resolution.  **Note**: This method must be implemented for Market API Capability Level 2. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subscriptionId"></param> 
        /// <param name="queryId"></param> 
        /// <returns></returns>            
        public void PostQueryReplyDemands (PropertyQueryReply body, string subscriptionId, string queryId)
        {
            // verify the required parameter 'body' is set
            if (body == null) throw new ApiException(400, "Missing required parameter 'body' when calling PostQueryReplyDemands");
            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null) throw new ApiException(400, "Missing required parameter 'subscriptionId' when calling PostQueryReplyDemands");
            // verify the required parameter 'queryId' is set
            if (queryId == null) throw new ApiException(400, "Missing required parameter 'queryId' when calling PostQueryReplyDemands");
            
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
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryReplyDemands: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling PostQueryReplyDemands: " + response.ErrorMessage, response.ErrorMessage);
    
            return;
        }
    
        /// <summary>
        /// Rejects Proposal (Offer). Effectively ends a Negotiation chain - it explicitly indicates that the sender will not create another counter-Proposal. 
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
        /// Publishes Requestor capabilities via Demand. Demand object can be considered an \&quot;open\&quot; or public Demand, as it is not directed at a specific Provider, but rather is sent to the market so that the matching mechanism implementation can associate relevant Offers.  **Note**: it is an \&quot;atomic\&quot; operation, ie. as soon as Subscription is placed, the Demand is published on the market. 
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
    
            return JsonConvert.DeserializeObject(response.Content, typeof(String)) as String;
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
        /// Stop subscription for previously published Demand. Stop receiving Proposals.  **Note**: this will terminate all pending &#x60;collectOffers&#x60; calls on this subscription. This implies, that client code should not &#x60;unsubscribeDemand&#x60; before it has received all expected/useful inputs from &#x60;collectOffers&#x60;. 
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
        /// Waits for Agreement approval by the Provider. This is a blocking operation. The call may be aborted by Requestor caller code. After the call is aborted, another &#x60;waitForApproval&#x60; call can be raised on the same Agreement Id.  It returns one of the following options: * &#x60;Ok&#x60; - Indicates that the Agreement has been approved by the Provider.   - The Provider is now ready to accept a request to start an Activity     as described in the negotiated agreement.   - The Requestor’s corresponding &#x60;waitForApproval&#x60; call returns Ok after     this on the Provider side.  * &#x60;Rejected&#x60; - Indicates that the Provider has called &#x60;rejectAgreement&#x60;,   which effectively stops the Agreement handshake. The Requestor may attempt   to return to the Negotiation phase by sending a new Proposal.  * &#x60;Cancelled&#x60; - Indicates that the Requestor himself has called  &#x60;cancelAgreement&#x60;, which effectively stops the Agreement handshake. 
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
    
            return JsonConvert.DeserializeObject(response.Content, typeof(String)) as String;
        }
    
    }
}
