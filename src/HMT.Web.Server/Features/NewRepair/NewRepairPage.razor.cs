using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Interfaces;
using HMT.Web.Server.Models.Entities;

namespace HMT.Web.Server.Features.NewRepair
{
    public partial class NewRepairPage : ComponentBase
    {
        RepairOrder _repairModel = new RepairOrder();
        // bool _showSuccess = false;

        private async Task HandleFormSubmit(RepairOrder order)
        {
            // Mediate your request
            var command = new Command() { SomeUniqueThingInDb = order.SomeUniqueThingInDb, Reason = order.Reason };
            await Mediator.Send(command);

            // At the end, clear the form with empty new model
            _repairModel = new RepairOrder();
        }

        public record Command : IRequest<int>
        {
            public string SomeUniqueThingInDb { get; set; } = string.Empty;
            public string Reason { get; set; } = string.Empty;
        }

        public class Validator : AbstractValidator<Command>
        {
            private readonly IInMemoryDatabase _db;
            public Validator(IInMemoryDatabase db) : base()
            {
                _db = db;
            }

            public Validator()
            {
                RuleFor(repairCommand => repairCommand.SomeUniqueThingInDb)
                    .MustAsync(BeUniqueSomething)
                    .WithMessage($"{nameof(RepairOrder.SomeUniqueThingInDb)} must be unique.");
            }

            public async Task<bool> BeUniqueSomething(Command repairCommand, string someUniqueThingInDb, CancellationToken cancellationToken)
            {
                return (await _db.GetRepairOrders())
                    .Any(ro => ro.SomeUniqueThingInDb != someUniqueThingInDb);
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IInMemoryDatabase _db;
            public Handler(IInMemoryDatabase db)
            {
                _db = db;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new RepairOrder
                {
                    SomeUniqueThingInDb = request.SomeUniqueThingInDb,
                    Reason = request.Reason
                };

                // Try to put business logic related to RepairOrder inside RepairOrder (for eg: Stuffs like GetCostOfRepairOrder() method inside it)
                // but if some logic that wasn't appropriate to put there will live here.

                //_db.RepairOrders.Add(entity);

                // await _db.SaveChangesAsync(cancellationToken);

                return entity.OrderId;
            }
        }
    }
}
