using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.GetAllMerchendise
{
    public sealed class GetAllMerchendiseCommand: IRequest<GetAllMerchendiseResponse>
    {
        public long EmployeeId { get; init; }
    }
}