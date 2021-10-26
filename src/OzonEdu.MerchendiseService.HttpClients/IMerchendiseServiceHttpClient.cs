using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.HttpModels;

namespace OzonEdu.MerchendiseService.HttpClients
{
    public interface IMerchendiseServiceHttpClient
    {
        Task<MerchendiseInfoResponse?> V1GetAllMerchendise(long employeeId, CancellationToken token);
        Task<MerchendiseInfoResponse?> V1RequestMerchendise(MerchendiseRequest merchendiseRequest, CancellationToken token);
    }
}