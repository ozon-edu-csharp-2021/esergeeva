using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchendiseService.HttpModels;

namespace OzonEdu.MerchendiseService.Presentation.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merch")]
    [Produces("application/json")]
    public class MerchendiseController : ControllerBase
    {
        [HttpGet("get-all", Name = nameof(GetAllMerchendise))]
        public ActionResult<GetAllMerchendiseResponse> GetAllMerchendise(long employeeId)
        {
            return Ok(new GetAllMerchendiseResponse());
        }

        [HttpPost("request", Name = nameof(RequestMerchendise))]
        public ActionResult<RequestMerchendiseResponse> RequestMerchendise([FromBody] RequestMerchendiseRequest merchendiseRequest)
        {
            return Ok(new RequestMerchendiseResponse());
        }
    }
}