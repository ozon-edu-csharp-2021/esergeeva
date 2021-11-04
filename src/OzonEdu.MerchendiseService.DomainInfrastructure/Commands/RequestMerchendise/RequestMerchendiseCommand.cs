using CSharpCourse.Core.Lib.Enums;
using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise
{
    public class RequestMerchendiseCommand: IRequest<RequestMerchendiseResponse>
    {
        public long EmployeeId { get; init; }
        public MerchType MerchendisePackType { get; init; }
    }
}