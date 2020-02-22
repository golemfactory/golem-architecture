using Golem.MarketApi.Client.Swagger.Api;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
using Golem.Provider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GolemSampleProvider1.Processor
{
    public class MarketListener
    {
        public ApiClient ApiClient { get; protected set; }

        public ProviderApi ProviderClient { get; protected set; }

        public IAgreementRepository AgreementRepository { get; set; }

        public MarketListener(ApiClient client, IAgreementRepository agreementRepository)
        {
            this.ApiClient = client;
            this.ProviderClient = new ProviderApi(client);
            this.AgreementRepository = agreementRepository;
        }

        public void Run()
        {
            Console.WriteLine("Transcoding Offer Subscription - Simple Demo");

            try
            {
                // Start by proposing a generic offer on the market

                var genericOffer = new Offer()
                {
                    Properties = Resources.Transcoding_Offer_Generic,
                    Constraints = "()"
                };

                Console.WriteLine("Offer composed:");
                Console.WriteLine($"{genericOffer}");
                Console.WriteLine("Subscribing to market...\n");

                var offerSubscriptionId = this.ProviderClient.SubscribeOffer(genericOffer);

                Console.WriteLine($"Offer sent to market, SubscriptionId: {offerSubscriptionId}");

                Console.Write("Fetching all Offer subscriptions...");

                var offerSubscriptions = this.ProviderClient.GetOffers();

                Console.WriteLine($" there are {offerSubscriptions.Count} active Offer subscriptions.");


                Console.WriteLine("Collecting proposals...");


                // Collect proposals until they arrive finally...

                List<Event> proposals = null;

                while (true) // repeat until Agreement proposal received and approved
                {
                    do
                    {
                        proposals = this.ProviderClient.CollectDemands(offerSubscriptionId, 1000, 10);  // Timeout and maxCount should be ints (are floats now)!!!!
                        if (proposals.Count() == 0)
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

                    // For the first proposal received:
                    // If demand proposal received, issue counter offer (more detailed)
                    // If agreement proposal received - accept immediately

                    var provEvent = proposals.First();

                    if (provEvent is ProposalEvent)
                    {
                        var demandProposal = proposals[0] as ProposalEvent;

                        var pricedProposal = new Proposal()
                        {
                            Properties = Resources.Transcoding_Offer_Priced,
                            Constraints = "()"
                        };

                        Console.WriteLine("\nCounter Offer (priced) composed:");
                        Console.WriteLine($"{pricedProposal}");
                        Console.WriteLine("Sending counter-Offer...\n");

                        var offerProposalId = this.ProviderClient.CreateProposalOffer(pricedProposal, offerSubscriptionId, demandProposal.Proposal.ProposalId);

                    }
                    else if (provEvent is AgreementEvent)
                    {
                        Console.WriteLine("\nApproving proposed agreement...");
                        var agreementEvent = provEvent as AgreementEvent;
                        this.ProviderClient.ApproveAgreement(agreementEvent.Agreement.AgreementId);

                        var agreementDetail = this.ProviderClient.GetAgreement(agreementEvent.Agreement.AgreementId);

                        Console.WriteLine("\nApproved agreement:");
                        Console.WriteLine(agreementDetail);


                        //* this is prototype code, should be removed, as there should be proper mappers for MarketAPI Models to Golem.Provider.Entities

                        var agreementEntity = new Golem.Provider.Entities.Agreement()
                        {
                            AgreementId = agreementEvent.Agreement.AgreementId,
                            Demand = new Golem.Provider.Entities.Demand()
                            {
                                Id = agreementEvent.Agreement.AgreementId,
                                Constraints = agreementEvent.Agreement.Demand.Constraints,
                                Properties = agreementEvent.Agreement.Demand.Properties as Dictionary<string, string>
                            },
                            Offer = new Golem.Provider.Entities.Offer()
                            {
                                Constraints = agreementEvent.Agreement.Offer.Constraints,
                                Properties = agreementEvent.Agreement.Offer.Properties as Dictionary<string, string>
                            }
                        };

                        this.AgreementRepository.SaveAgreement(agreementEntity);

                        Console.WriteLine("Agreement approved!");
                        break;
                    }

                }

                Console.WriteLine("\nClosing subscription...");
                this.ProviderClient.UnsubscribeOffer(offerSubscriptionId);

                Console.WriteLine("Subscription closed.\n");

            }
            catch (Exception exc)
            {
                Console.WriteLine($"{exc}");
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }

    }
}
