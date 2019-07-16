using Golem.MarketApi.Client.Swagger.Api;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
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

        public MarketListener(ApiClient client)
        {
            this.ApiClient = client;
            this.ProviderClient = new ProviderApi(client);
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

                var offerSubscriptionId = this.ProviderClient.Subscribe(genericOffer);

                Console.WriteLine($"Offer sent to market, SubscriptionId: {offerSubscriptionId}");

                Console.WriteLine("Collecting proposals...");


                // Collect proposals until they arrive finally...

                List<ProviderEvent> proposals = null;

                while (true) // repeat until Agreement proposal received and approved
                {
                    do
                    {
                        proposals = this.ProviderClient.Collect(offerSubscriptionId, 1000, 10);  // Timeout and maxCount should be ints (are floats now)!!!!
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

                    if (provEvent is DemandEvent)
                    {
                        var demandProposal = proposals[0] as DemandEvent;

                        var pricedProposal = new Proposal()
                        {
                            Properties = Resources.Transcoding_Offer_Priced,
                            Constraints = "()"
                        };

                        Console.WriteLine("\nCounter Offer (priced) composed:");
                        Console.WriteLine($"{pricedProposal}");
                        Console.WriteLine("Sending counter-Offer...\n");

                        var offerProposalId = this.ProviderClient.CreateProposal(offerSubscriptionId, demandProposal.Demand.Id, pricedProposal);

                    }
                    else if (provEvent is NewAgreementEvent)
                    {
                        Console.WriteLine("\nApproving proposed agreement...");
                        var agreementEvent = provEvent as NewAgreementEvent;
                        this.ProviderClient.ApproveAgreement(agreementEvent.AgreementId);

                        Console.WriteLine("Agreement approved!");
                        break;
                    }

                }

                Console.WriteLine("\nClosing subscription...");
                this.ProviderClient.Unsubscribe(offerSubscriptionId);

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
