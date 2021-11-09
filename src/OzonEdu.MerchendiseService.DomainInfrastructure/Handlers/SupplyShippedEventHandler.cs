using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.OuterCommands;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers
{
    public class SupplyShippedEventHandler : IRequestHandler<SupplyShippedEventCommand>
    {
        private readonly IMerchendiseRequestRepository _merchendiseRequestRepository;

        public SupplyShippedEventHandler(IMerchendiseRequestRepository merchendiseRequestRepository)
        {
            _merchendiseRequestRepository = merchendiseRequestRepository;
        }

        public async Task<Unit> Handle(SupplyShippedEventCommand request, CancellationToken cancellationToken)
        {
            var merchendiseInProgress =
                await _merchendiseRequestRepository.FindAllByStatus(MerchendiseRequestStatus.InProgress,
                    cancellationToken);
            // TODO Try to request merch from stock-api
            return new Unit();
        }
    }
}