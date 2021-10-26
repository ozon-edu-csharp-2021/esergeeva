using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchendiseService.HttpModels;

namespace OzonEdu.MerchendiseService.Presentation.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchendiseController : ControllerBase
    {
        [HttpGet("get-all", Name = nameof(GetRequestedMerchendise))]
        public ActionResult<MerchendiseInfoResponse> GetRequestedMerchendise(long employeeId)
        {
            return Ok(new MerchendiseInfoResponse());
        }

        [HttpPost("request", Name = nameof(RequestMerchendise))]
        public ActionResult<MerchendiseInfoResponse> RequestMerchendise([FromBody] MerchendiseRequest merchendiseRequest)
        {
            return Ok(new MerchendiseInfoResponse());
        }
    }
}