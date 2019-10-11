using FakeItEasy;
using Golem.ActivityApi.Client.Swagger.Api;
using Golem.ActivityApi.Client.Swagger.Model;
using Golem.Provider.ActivityControl;
using Golem.Provider.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Golem.Provider.Tests
{
    [TestClass]
    public class ActivityControlerTests
    {
        [TestMethod]
        public void ActivityController_ShouldCreateActivity_Successfully()
        {

            string agreementId = "" + Guid.NewGuid();

            var activityApi = A.Fake<IProviderGatewayApi>();

            var activityId = Guid.NewGuid();

            A.CallTo(() => activityApi.CollectActivityEvents(null))
                .WithAnyArguments()
                .Returns(new List<ProviderEvent>()
                {
                    new CreateActivityProviderEvent()
                    {
                        ActivityId = ""+activityId,
                        EventType = "CreateActivity",
                        AgreementId = agreementId
                    }
                });

            var exeUnit = A.Fake<IExeUnit>();

            var exeUnitFactory = A.Fake<IExeUnitFactory>();

            A.CallTo(() => exeUnitFactory.BuildExeUnit(null))
                .WithAnyArguments()
                .Returns(exeUnit);

            // build agreement repo

            var agreementRepository = A.Fake<IAgreementRepository>();

            var agreement = new Entities.Agreement()
            {
                AgreementId = agreementId
            };

            A.CallTo(() => agreementRepository.GetAgreement(agreementId)).Returns(agreement);

            // build activity repo

            var activityRepository = A.Fake<IActivityRepository>();

            var controller = new ActivityController(activityApi, exeUnitFactory, agreementRepository, activityRepository);

            Task.Run(() => controller.Run());

            Thread.Sleep(1000); // CreateActivity should be complete by now

            // Run assertions

            A.CallTo(() => exeUnit.CreateActivity(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => activityRepository.CreateActivity(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();




        }

        [TestMethod]
        public void ActivityController_ShouldCreateActivity_FailGracefully()
        {

            string agreementId = "" + Guid.NewGuid();

            var activityApi = A.Fake<IProviderGatewayApi>();

            var activityId = Guid.NewGuid();

            A.CallTo(() => activityApi.CollectActivityEvents(null))
                .WithAnyArguments()
                .Returns(new List<ProviderEvent>()
                {
                    new CreateActivityProviderEvent()
                    {
                        ActivityId = ""+activityId,
                        EventType = "CreateActivity",
                        AgreementId = agreementId
                    }
                });

            var exeUnit = A.Fake<IExeUnit>();

            // Simulate ExeUnit error on CreateActivity
            A.CallTo(() => exeUnit.CreateActivity(null))
                .WithAnyArguments()
                .Returns(new Entities.CreateActivityResult()
                {
                    Result = Entities.ExeUnitResultType.Error
                }
                );

            var exeUnitFactory = A.Fake<IExeUnitFactory>();

            A.CallTo(() => exeUnitFactory.BuildExeUnit(null))
                .WithAnyArguments()
                .Returns(exeUnit);

            // build agreement repo

            var agreementRepository = A.Fake<IAgreementRepository>();

            var agreement = new Entities.Agreement()
            {
                AgreementId = agreementId
            };

            A.CallTo(() => agreementRepository.GetAgreement(agreementId)).Returns(agreement);

            // build activity repo

            var activityRepository = A.Fake<IActivityRepository>();

            var controller = new ActivityController(activityApi, exeUnitFactory, agreementRepository, activityRepository);

            Task.Run(() => controller.Run());

            Thread.Sleep(1000); // CreateActivity should be complete by now

            // Run assertions

            A.CallTo(() => exeUnit.CreateActivity(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => activityRepository.CreateActivity(null))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => activityRepository.SetActivityState(null, Entities.ActivityState.Terminated))
                .WithAnyArguments()
                .MustHaveHappenedOnceExactly();



        }


    }
}
