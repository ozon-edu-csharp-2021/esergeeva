using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchendiseService.Grpc;

namespace OzonEdu.MerchendiseService.Presentation.GrpcServices
{
    public class MerchendiseGrpcService : MerchendiseServiceGrpc.MerchendiseServiceGrpcBase
    {
        public override Task<GetAllMerchendiseResponse> GetAllMerchendise(GetAllMerchendiseRequest request,
            ServerCallContext context)
        {
            return Task.FromResult(new GetAllMerchendiseResponse());
        }

        public override Task<RequestMerchendiseResponse> RequestMerchendise(RequestMerchendiseRequest request,
            ServerCallContext context)
        {
            return Task.FromResult(new RequestMerchendiseResponse());
        }
    }
}