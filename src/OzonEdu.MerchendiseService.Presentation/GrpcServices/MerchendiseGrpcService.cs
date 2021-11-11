using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.GetAllMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;
using OzonEdu.MerchendiseService.Grpc;
using OzonEdu.MerchendiseService.Presentation.Extensions;
using GetAllMerchendiseResponse = OzonEdu.MerchendiseService.Grpc.GetAllMerchendiseResponse;
using MerchendiseRequestInfo = OzonEdu.MerchendiseService.Grpc.MerchendiseRequestInfo;
using RequestMerchendiseResponse = OzonEdu.MerchendiseService.Grpc.RequestMerchendiseResponse;

namespace OzonEdu.MerchendiseService.Presentation.GrpcServices
{
    public class MerchendiseGrpcService : MerchendiseServiceGrpc.MerchendiseServiceGrpcBase
    {
        private readonly IMediator _mediator;

        public MerchendiseGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAllMerchendiseResponse> GetAllMerchendise(GetAllMerchendiseRequest request,
            ServerCallContext context)
        {
            var command = new GetAllMerchendiseCommand
            {
                EmployeeId = request.EmployeeId
            };
            var result = await _mediator.Send(command, context.CancellationToken);

            var response = new GetAllMerchendiseResponse();
            response.MerchendiseRequests.AddRange(result.MerchendiseRequests.Select(requestInfo =>
                new MerchendiseRequestInfo
                {
                    RequestId = requestInfo.RequestId,
                    MerchendisePackType = requestInfo.MerchendisePackType.ConvertToGrpc(),
                    RequestStatus = requestInfo.RequestStatus.ConvertToGrpc()
                }));

            return response;
        }

        public override async Task<RequestMerchendiseResponse> RequestMerchendise(RequestMerchendiseRequest request,
            ServerCallContext context)
        {
            var command = new RequestMerchendiseCommand()
            {
                EmployeeId = request.EmployeeId,
                MerchendisePackType = request.MerchendisePackType.Convert()
            };
            var result = await _mediator.Send(command, context.CancellationToken);

            var response = new RequestMerchendiseResponse
            {
                MerchendiseRequestInfo = new MerchendiseRequestInfo
                {
                    RequestId = result.MerchendiseRequestInfo.RequestId,
                    MerchendisePackType = result.MerchendiseRequestInfo.MerchendisePackType.ConvertToGrpc(),
                    RequestStatus = result.MerchendiseRequestInfo.RequestStatus.ConvertToGrpc()
                }
            };

            return response;
        }
    }
}