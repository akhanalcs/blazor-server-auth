using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Data;
using HMT.Web.Server.Models.Entities;
using Moq;
using Shouldly;

namespace HMT.UnitTests.RepairOrders
{
    public class RepairOrderTests
    {
        [Fact]
        public void Should_Calculate_Correct_Cost()
        {
            // ARRANGE
            var repairOrder = new RepairOrder() { Reason = "Crash", SomeUniqueThingInDb = "Something" };

            // ACT
            var cost = repairOrder.GetCostOfRepairOrder();

            // ASSERT
            Assert.Equal(2000, cost);
        }
    }
}