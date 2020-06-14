using CommandLineParser.Core;
using Golem.Provider.ActivityControl;
using Golem.Provider.Repository;
using GolemSampleProvider1.Processor;
using System;
using System.Threading.Tasks;

namespace GolemSampleProvider1
{
    class Program
    {
        static void Main(string[] args)
        {
            // CLI parameters

            var parser = new FluentCommandLineParser();

            string apiKey = null;
            string marketUrl = null;
            string activityUrl = null;

            parser.Setup<string>("app-key").WithDescription("Use API Key for authorization to Golem APIs")
                .Callback(val => apiKey = val)
                .SetDefault(null);

            parser.Setup<string>("market-url").WithDescription("Market API URL base")
                .Callback(val => marketUrl = val)
                .SetDefault("http://localhost:5001/market-api/v1");

            parser.Setup<string>("activity-url").WithDescription("Activity API URL base")
                .Callback(val => activityUrl = val)
                .SetDefault("http://localhost:5001/activity-api/v1");


            parser.SetupHelp("?", "help")
                .Callback(text => {
                    Console.WriteLine(text);
                    Environment.Exit(0);
                    });

            parser.Parse(args);

            // Build client proxies

            var marketClient = new Golem.MarketApi.Client.Swagger.Client.ApiClient(marketUrl);
            var activityClient = new Golem.ActivityApi.Client.Swagger.Client.ApiClient(activityUrl);

            if (apiKey != null)
            {
                marketClient.AddDefaultHeader("Authorization", $"Bearer {apiKey}");
                activityClient.AddDefaultHeader("Authorization", $"Bearer {apiKey}");
            }

            // Run processing

            var agreementRepository = new InMemoryAgreementRepository();
            var activityRepository = new InMemoryActivityRepository();

            var exeUnitFactory = new DummyExeUnitFactory();

            var processor = new MarketListener(marketClient, agreementRepository);

            Task.Run(() => processor.Run());


            var activityApi = new Golem.ActivityApi.Client.Swagger.Api.ProviderGatewayApi(activityClient);



            var activityController = new ActivityController(activityApi, exeUnitFactory, agreementRepository, activityRepository);

            Task.Run(() => activityController.Run());

            Console.ReadKey();
        }
    }
}
