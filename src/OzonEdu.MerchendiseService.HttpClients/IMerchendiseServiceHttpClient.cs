using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.HttpModels;

namespace OzonEdu.MerchendiseService.HttpClients
{
    public interface IMerchendiseServiceHttpClient
    {
        Task<GetAllMerchendiseResponse?> V1GetAllMerchendise(long employeeId, CancellationToken token);
        Task<RequestMerchendiseResponse?> V1RequestMerchendise(RequestMerchendiseRequest merchendiseRequest, CancellationToken token);
    }
}