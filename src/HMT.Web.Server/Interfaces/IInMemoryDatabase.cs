using HMT.Web.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Interfaces
{
    public interface IInMemoryDatabase
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(DateTime startDate);

        Task<IEnumerable<RepairOrder>> GetRepairOrders();
    }
}
