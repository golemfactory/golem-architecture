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

            // Now proceed with Activity

            var activityApi = new Golem.ActivityApi.Client.Swagger.Api.ControlApi(activityClient);

            var activityId = activityApi.CreateActivity(agreementId);

            var batchId = activityApi.Exec(activityId, new Golem.ActivityApi.Client.Swagger.Model.ExeScriptRequest()
            {
                Text = "DEPLOY\nSTART"
            });

            // loop trying to collect Exec results

            List<Golem.ActivityApi.Client.Swagger.Model.ExeScriptCommandResult> batchResults;

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
                        Console.WriteLine($"Exec result: [{result.Index}] ({result.Result}), {result.Message}");

                    }
                }
            }
            while (batchResults.Count == 0);





            Console.WriteLine("Press any key...");
            Console.ReadKey();

        }


    }
}
