using Golem.MarketApi.Client.Swagger.Api;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
using GolemSampleApp1.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GolemSampleApp1.Processor
{
    public class MarketProcessor
    {
        public ApiClient ApiClient { get; protected set; }
        public RequestorApi RequestorClient { get; protected set; }

        public ProviderApi ProviderClient { get; protected set; }

        public DemandProvider DemandProvider { get; protected set; }

        public MarketProcessor(ApiClient client, DemandProvider.ScenarioEnum scenario)
        {
            this.ApiClient = client;
            this.RequestorClient = new RequestorApi(client);
            this.ProviderClient = new ProviderApi(client);
            this.DemandProvider = new DemandProvider(scenario);
        }

        public string Run()
        {
            // -- simulate generic offer being placed on market by provider

            //var offer = new Offer()
            //{
            //    Properties = Resources.Transcoding_Offer_Generic,
            //    Constraints = ""
            //};

            //var offerSubscriptionId= this.ProviderClient.Subscribe(offer);


            // -- start requestor interaction



            Console.WriteLine("Transcoding Demand Subscription - Simple Demo");

            try
            {
                var demand = new Demand()
                {
                    Properties = this.DemandProvider.GetDemandProperties(DemandProvider.NegotiationStageEnum.Start),
                    Constraints = this.DemandProvider.GetDemandConstraints(DemandProvider.NegotiationStageEnum.Start)
                };

                Console.WriteLine("Demand composed:");
                Console.WriteLine($"{demand}");
                Console.WriteLine("Subscribing to market...\n");

                var demandSubscriptionId = this.RequestorClient.SubscribeDemand(demand);

                Console.WriteLine($"Demand sent to market, SubscriptionId:{demandSubscriptionId}");

                Console.Write("Fetching all Demand subscriptions...");

                var demandSubscriptions = this.RequestorClient.GetDemands();

                Console.WriteLine($" there are {demandSubscriptions.Count} active Demand subscriptions.");


                Console.WriteLine("Collecting proposals...");

                // Collect proposals until they arrive finally...

                List<ProposalEvent> proposals = null;

                do
                {
                    proposals = this.RequestorClient.CollectOffers(demandSubscriptionId, 1000, 10).Select(prop => prop as ProposalEvent).ToList();  // Timeout and maxCount should be ints!!!!
                    if(proposals.Count() == 0)
                    {
                        Console.WriteLine("No proposals received, keep listening...");
                    }
                }
                while (proposals.Count() == 0);

                Console.WriteLine($"Received {proposals.Count} proposals:");

                proposals.ForEach(proposal =>
                {
                    Console.WriteLine($"{proposal}");
                });

                // Allow user to interact and select offer to negotiate...

                int proposalIndex = -1;
                do
                {
                    Console.Write($"Select proposal ({1}-{proposals.Count}): ");
                    var proposalNoText = Console.ReadLine();

                    Int32.TryParse(proposalNoText, out proposalIndex);
                }
                while (proposalIndex <= 0 || proposalIndex > proposals.Count);

                Console.WriteLine();

                var agreementId = this.Negotiate_AsItIs(demandSubscriptionId, ""/*offerSubscriptionId*/, proposals[proposalIndex-1]);

                // -- close the offer subscription
                Console.WriteLine("\nClosing subscription...");
                this.RequestorClient.UnsubscribeDemand(demandSubscriptionId);

                Console.WriteLine("Subscription closed.\n");

                return agreementId;
            }
            catch(Exception exc)
            {
                Console.WriteLine($"{exc}");
            }

            return null;

        }


        protected string Negotiate_AsItIs(string demandSubscriptionId, string offerSubscriptionId, ProposalEvent offerProposal)
        {
            // -- Create Agreement 

            var selectedProposal = this.RequestorClient.GetProposalOffer(demandSubscriptionId, offerProposal.Proposal.ProposalId);

            Console.WriteLine("\nSelected Offer proposal:");

            Console.WriteLine($"{selectedProposal}");


            Console.WriteLine("Negotiating...");


            var demandProposal = new Proposal()
            {
                Properties = this.DemandProvider.GetDemandProperties(DemandProvider.NegotiationStageEnum.Negotiate),
                Constraints = this.DemandProvider.GetDemandConstraints(DemandProvider.NegotiationStageEnum.Negotiate)
            };

            var curDemandProposalId = this.RequestorClient.CreateProposalDemand(demandProposal, demandSubscriptionId, offerProposal.Proposal.ProposalId);

            // ---- now we should observe the offer counter proposal with price - on Requestor side

            List<ProposalEvent> offerProposals = null;

            ProposalEvent newOfferProposal = null;

            do
            {
                var offerEvents = this.RequestorClient.CollectOffers(demandSubscriptionId, 1000, 10).ToList();
                offerProposals = offerEvents.Select(prop => prop as ProposalEvent).ToList();  // Timeout and maxCount should be ints!!!!
                if (offerProposals.Count() == 0)
                {
                    Console.WriteLine("No proposals received, keep listening...");
                }

                newOfferProposal = offerProposals.Select(prop => prop as ProposalEvent).Where(prop => prop.Proposal.PrevProposalId == curDemandProposalId).FirstOrDefault();


            }
            while (offerProposals.Count() == 0 || newOfferProposal == null);

            Console.WriteLine($"Received counter offer proposal in negotiation:");

            Console.WriteLine($"{newOfferProposal}");


            // -- Now propose agreement!

            Console.WriteLine("Creating Agreement...");

            var agreement = new AgreementProposal()
            {
                ProposalId = offerProposal.Proposal.ProposalId,
                ValidTo = DateTime.Now.AddMinutes(1)
            };

            this.RequestorClient.CreateAgreement(agreement);
            var agreementId = agreement.ProposalId;

            Console.WriteLine($"Agreement created with AgreementId: {agreementId}");

            Console.WriteLine("Confirm Agreement...");
            this.RequestorClient.ConfirmAgreement(agreementId);

            Console.WriteLine("Waiting for Agreement response...");

            try
            {
                String result = "";
                while ((result = this.RequestorClient.WaitForApproval(agreementId, 10000)) == "Timeout")
                {
                    Console.WriteLine("Waiting for Agreement approval...");

                }

                switch(result)
                {
                    case "Approved":
                        Console.WriteLine("Agreement approved!");


                        var agreementApproved = this.RequestorClient.GetAgreement(agreementId);

                        Console.WriteLine("Agreement: " + agreementApproved);

                        break;
                    case "Rejected":
                        Console.WriteLine("Agreement rejected!");
                        break;
                    case "Cancelled":
                        Console.WriteLine("Agreement cancelled!");
                        break;
                    default:
                        Console.WriteLine($"Unknown response {result} !!!");
                        break;


                }

                return agreementId;
            }
            catch (ApiException exc)
            {
                if(exc.ErrorCode == 406)
                {
                    Console.WriteLine("Agreement rejected!");
                }
                if (exc.ErrorCode == 408)
                {
                    Console.WriteLine("Timeout waiting for agreement approval...");
                }
            }

            return null;
        }

        protected void Negotiate_AsItShouldBe(string demandSubscriptionId, string offerSubscriptionId, Proposal offerProposal)
        {

            string curOfferProposalId = offerProposal.ProposalId;

            var demandProposal = new Proposal()
            {
                Properties = this.DemandProvider.GetDemandProperties(DemandProvider.NegotiationStageEnum.Negotiate),
                Constraints = this.DemandProvider.GetDemandConstraints(DemandProvider.NegotiationStageEnum.Negotiate)
            };

            var curDemandProposalId = this.RequestorClient.CreateProposalDemand(demandProposal, curOfferProposalId, demandSubscriptionId);

            // ---- now we should observe the offer counter proposal with price - on Requestor side

            var offerProposals = this.RequestorClient.CollectOffers(demandSubscriptionId, 1000, 10);

            var newOfferProposal = offerProposals.Select(prop => prop as ProposalEvent).Where(prop => prop.Proposal.PrevProposalId == curDemandProposalId).FirstOrDefault();

            if(newOfferProposal!= null)
            {
                // Accept proposal and send agreement

                var agreement = new AgreementProposal()
                {
                    ProposalId = newOfferProposal.Proposal.ProposalId
                };

                this.RequestorClient.CreateAgreement(agreement);


            }


        }

    }
}
