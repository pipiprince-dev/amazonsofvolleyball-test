namespace Backend.WebAPI.Controllers
{
    [ApiController]
    public class SecurityConroller : ControllerBase
    {
        private readonly ISecurityService securityService;

        public SecurityConroller(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "login")]
        [Route("api/v{version:apiVersion}/security/login")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPost]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(JWTokenModel))]
        [ProducesResponseType((int)HttpResponseType.Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> LoginAsync([FromBody][Required] LoginModel requestParam)
        {
            try
            {
                var response = await securityService.LoginAsync(requestParam);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "register")]
        [Route("api/v{version:apiVersion}/security/register")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPost]
        [ProducesResponseType((int)HttpResponseType.OK)]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> RegisterAsync([FromBody][Required] RegisterModel requestParam)
        {
            try
            {
                await securityService.RegisterAsync(requestParam);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "registerAdmin")]
        [Route("api/v{version:apiVersion}/security/registerAdmin")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPost]
        [ProducesResponseType((int)HttpResponseType.OK)]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody][Required] RegisterModel requestParam)
        {
            try
            {
                await securityService.RegisterAdminAsync(requestParam);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
