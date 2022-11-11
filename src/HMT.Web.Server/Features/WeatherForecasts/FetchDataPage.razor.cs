using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Data;
using HMT.Web.Server.Interfaces;
using HMT.Web.Server.Models.Entities;

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

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(m => m.StartDate).NotNull();
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<WeatherForecast>>
        {
            private readonly IInMemoryDatabase _db;
            public Handler(IInMemoryDatabase db)
            {
                _db = db;
            }

            public async Task<IEnumerable<WeatherForecast>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.GetWeatherForecasts(request.StartDate);
            }
        }
    }
}
