using Golem.MarketApi.Client.Swagger.Api;
using Golem.MarketApi.Client.Swagger.Client;
using Golem.MarketApi.Client.Swagger.Model;
using GolemSampleApp1.Processor;
using System;

namespace GolemSampleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient("http://localhost:5001/market-api/v1");

            var processor = new MarketProcessor(client);

            processor.Run();
        }


    }
}
