using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.HttpModels;

namespace OzonEdu.MerchendiseService.HttpClients
{
    public class MerchendiseServiceHttpClient : IMerchendiseServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchendiseServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetAllMerchendiseResponse> V1GetAllMerchendise(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/get-all?employeeId={employeeId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<GetAllMerchendiseResponse>(body);
        }

        public async Task<RequestMerchendiseResponse> V1RequestMerchendise(RequestMerchendiseRequest merchendiseRequest, CancellationToken token)
        {
            var content = JsonContent.Create(merchendiseRequest);
            using var response = await _httpClient.PostAsync("v1/api/merch/request", content, token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<RequestMerchendiseResponse>(body);
        }
    }
}