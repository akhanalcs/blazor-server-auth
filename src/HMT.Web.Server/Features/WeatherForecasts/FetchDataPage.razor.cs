using MediatR;
using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Data;
using HMT.Web.Server.Models;

namespace HMT.Web.Server.Features.WeatherForecasts
{
    public partial class FetchDataPage : ComponentBase
    {
        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            var query = new Query() { StartDate = DateTime.Now };
            forecasts = (await Mediator.Send(query)).ToArray();
        }

        public record Query : IRequest<IEnumerable<WeatherForecast>>
        {
            public DateTime StartDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<WeatherForecast>>
        {
            private readonly WeatherForecastService _wfs;
            public Handler(WeatherForecastService wfs)
            {
                _wfs = wfs;
            }

            public async Task<IEnumerable<WeatherForecast>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _wfs.GetWeatherForecasts(request.StartDate);
            }
        }
    }
}
