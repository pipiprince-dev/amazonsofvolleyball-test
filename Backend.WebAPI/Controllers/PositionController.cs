namespace Backend.API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService; 
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "getAllAsync")]
        [Route("api/v{version:apiVersion}/positions/get-all")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpGet]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(List<PositionModel>))]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        public async Task<List<PositionModel>> UploadImageAsync()
        {
            return await positionService.GetAllAsync();
        }
    }
}
