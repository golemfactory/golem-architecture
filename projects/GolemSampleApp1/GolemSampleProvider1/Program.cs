using Golem.MarketApi.Client.Swagger.Client;
using GolemSampleProvider1.Processor;
using System;

namespace GolemSampleProvider1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient("http://localhost:5001/market-api/v1");

            var processor = new MarketListener(client);

            processor.Run();
        }
    }
}
