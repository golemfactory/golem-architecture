using CommandLineParser.Core;
using GolemSampleApp1.Processor;
using GolemSampleApp1.Providers;
using System;
using System.Collections.Generic;

namespace GolemSampleApp1
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
            DemandProvider.ScenarioEnum scenario = DemandProvider.ScenarioEnum.Transcoding;

            parser.Setup<string>("app-key").WithDescription("Use API Key for authorization to Golem APIs")
                .Callback(val => apiKey = val)
                .SetDefault(null);

            parser.Setup<string>("market-url").WithDescription("Market API URL base")
                .Callback(val => marketUrl = val)
                .SetDefault("http://localhost:5001/market-api/v1");

            parser.Setup<string>("activity-url").WithDescription("Activity API URL base")
                .Callback(val => activityUrl = val)
                .SetDefault("http://localhost:5001/activity-api/v1");

            parser.Setup<DemandProvider.ScenarioEnum>("scenario").WithDescription("Choose scenario (Transcoding, Wasm)")
                .Callback(val => scenario = val)
                .SetDefault(DemandProvider.ScenarioEnum.Transcoding);

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

            var processor = new MarketProcessor(marketClient, scenario);

            var agreementId = processor.Run();


            if(agreementId == null)
            {
                Console.WriteLine("Provider does not respond to Agreement proposal, exiting...");
                return;
            }
            // Now proceed with Activity

            var activityApi = new Golem.ActivityApi.Client.Swagger.Api.ControlApi(activityClient);

            Console.WriteLine("Creating Activity...");

            var activityId = activityApi.CreateActivity(agreementId);

            Console.WriteLine($"Activity {activityId} created.");

            var exeScriptText = "DEPLOY\nSTART";

            Console.WriteLine($"Calling Exec(\n{exeScriptText}\n) created.");


            var batchId = activityApi.Exec(activityId, new Golem.ActivityApi.Client.Swagger.Model.ExeScriptRequest()
            {
                Text = exeScriptText
            });

            // loop trying to collect Exec results

            List<Golem.ActivityApi.Client.Swagger.Model.ExeScriptCommandResult> batchResults;

            Console.WriteLine("Press any key to stop listening...");

            bool stop = false;

            do
            {
                batchResults = activityApi.GetExecBatchResults(activityId, batchId, 1000);

                if (batchResults.Count == 0)
                {
                    Console.WriteLine("No Exec results received, keep listening...");
                }
                else
                {
                    foreach(var result in batchResults)
                    {
                        Console.WriteLine($"Exec result: [{result}");

                        if(result.IsBatchFinished == true)
                        {
                            Console.WriteLine($"ExeScript batch complete.");
                            stop = true;
                        }

                    }
                }
            }
            while (!Console.KeyAvailable && !stop);

            Console.ReadKey();

        }

    }
}
