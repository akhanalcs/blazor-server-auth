using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Data;
using HMT.Web.Server.Models;

namespace HMT.Web.Server.Features.WeatherForecasts
{
    public partial class FetchDataPage : ComponentBase
    {
        [Inject]
        private WeatherForecastService ForecastService { get; set; } = default!;

        private IEnumerable<WeatherForecast>? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetWeatherForecasts(DateTime.Now);
        }
    }
}