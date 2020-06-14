using GolemSampleApp1.Processor;
using System;
using System.Collections.Generic;

namespace GolemSampleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var marketClient = new Golem.MarketApi.Client.Swagger.Client.ApiClient("http://localhost:5001/market-api/v1");
            var activityClient = new Golem.ActivityApi.Client.Swagger.Client.ApiClient("http://localhost:5001/activity-api/v1");

            var processor = new MarketProcessor(marketClient);

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
