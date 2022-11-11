using HMT.Web.Server.Interfaces;
using HMT.Web.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Data
{
    public class InMemoryDatabase : IInMemoryDatabase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate)
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            return forecasts;
        }

        public async Task<IEnumerable<RepairOrder>> GetRepairOrders()
        {
            return new RepairOrder[] {
                new RepairOrder
                {
                    OrderId = 1,
                    SomeUniqueThingInDb = "Ayo 1",
                    Reason = "Reason 1"
                },
                new RepairOrder
                {
                    OrderId = 2,
                    SomeUniqueThingInDb = "Ayo 2",
                    Reason = "Reason 2"
                }
            }
            .ToArray();
        }
    }
}
