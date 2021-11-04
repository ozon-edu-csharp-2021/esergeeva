using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.GetAllMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;
using OzonEdu.MerchendiseService.HttpModels;
using OzonEdu.MerchendiseService.Presentation.Extensions;
using DomainRequestStatus = OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models.RequestStatus;
using GetAllMerchendiseResponse = OzonEdu.MerchendiseService.HttpModels.GetAllMerchendiseResponse;
using RequestMerchendiseResponse = OzonEdu.MerchendiseService.HttpModels.RequestMerchendiseResponse;

namespace OzonEdu.MerchendiseService.Presentation.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchendiseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MerchendiseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all", Name = nameof(GetAllMerchendise))]
        public async Task<ActionResult<GetAllMerchendiseResponse>> GetAllMerchendise(long employeeId,
            CancellationToken cancellationToken)
        {
            var command = new GetAllMerchendiseCommand
            {
                EmployeeId = employeeId
            };

            var result = await _mediator.Send(command, cancellationToken);
            if (result.MerchendiseRequests.Count == 0)
                return NotFound();

            var response = new GetAllMerchendiseResponse
            {
                MerchendiseRequests = result.MerchendiseRequests.Select(request => new MerchendiseRequestInfo
                {
                    RequestId = request.RequestId,
                    MerchendisePackType = request.MerchendisePackType,
                    RequestStatus = request.RequestStatus.ConvertToHttp()
                }).ToList()
            };
            return Ok(response);
        }

        [HttpPost("request", Name = nameof(RequestMerchendise))]
        public async Task<ActionResult<RequestMerchendiseResponse>> RequestMerchendise(
            [FromBody] RequestMerchendiseRequest merchendiseRequest, CancellationToken cancellationToken)
        {
            var command = new RequestMerchendiseCommand()
            {
                EmployeeId = merchendiseRequest.EmployeeId,
                MerchendisePackType = merchendiseRequest.MerchendisePackType
            };

            var result = await _mediator.Send(command, cancellationToken);

            var response = new RequestMerchendiseResponse
            {
                MerchendiseRequestInfo = new MerchendiseRequestInfo
                {
                    RequestId = result.MerchendiseRequestInfo.RequestId,
                    MerchendisePackType = result.MerchendiseRequestInfo.MerchendisePackType,
                    RequestStatus = result.MerchendiseRequestInfo.RequestStatus.ConvertToHttp()
                }
            };
            return Ok(response);
        }
    }
}