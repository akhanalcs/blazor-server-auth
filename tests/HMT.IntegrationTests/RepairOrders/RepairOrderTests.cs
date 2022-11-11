using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMT.IntegrationTests.Attributes;
using HMT.Web.Server.Features.NewRepair;
using HMT.Web.Server.Features.ViewRepairs;
using HMT.Web.Server.Models.Entities;
using Shouldly;

namespace HMT.IntegrationTests.RepairOrders
{
    [Collection(nameof(TestFixture))]
    public class RepairOrderTests
    {
        private readonly TestFixture _fixture;

        public RepairOrderTests(TestFixture fixture) => _fixture = fixture;

        [IgnoreOnBuildServerFact]
        public async Task Should_Return_All_Repair_Orders()
        {
            // ARRANGE
            var repairOrder = new RepairOrder() { Reason = "Crash", SomeUniqueThingInDb = "Something" };
            await _fixture.SendAsync(new NewRepairPage.Command() { SomeUniqueThingInDb = repairOrder.SomeUniqueThingInDb, Reason = repairOrder.Reason });

            // ACT
            var repairOrderQuery = new ViewRepairsPage.Query();
            var result = await _fixture.SendAsync(repairOrderQuery);

            // ASSERT
            result.ShouldNotBeNull();
            // Other Asserts
        }
    }
}
