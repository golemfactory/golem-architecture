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
            var marketClient = new Golem.MarketApi.Client.Swagger.Client.ApiClient("http://localhost:5001/market-api/v1");

            var agreementRepository = new InMemoryAgreementRepository();
            var activityRepository = new InMemoryActivityRepository();

            var exeUnitFactory = new DummyExeUnitFactory();

            var processor = new MarketListener(marketClient, agreementRepository);

            Task.Run(() => processor.Run());

            var activityClient = new Golem.ActivityApi.Client.Swagger.Client.ApiClient("http://localhost:5001/activity-api/v1");

            var activityApi = new Golem.ActivityApi.Client.Swagger.Api.ProviderGatewayApi(activityClient);



            var activityController = new ActivityController(activityApi, exeUnitFactory, agreementRepository, activityRepository);

            Task.Run(() => activityController.Run());

            Console.ReadKey();
        }
    }
}
