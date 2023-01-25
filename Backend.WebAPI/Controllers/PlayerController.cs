namespace Backend.API.Controllers
{
    [Authorize]
    [ApiController]
    public partial class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "getAllAsync")]
        [Route("api/v{version:apiVersion}/players/get-all")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpGet]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(List<PlayerModel>))]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task<List<PlayerModel>> GetAll()
        {
            try
            {
                return await playerService.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "pagedAsync")]
        [Route("api/v{version:apiVersion}/players/page/{page}")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpGet]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(List<PlayerModel>))]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task<List<PlayerModel>> Page([FromRoute][Required] int page = 0)
        {
            try
            {
                return await playerService.PageAsync(page);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "getByIdAsync")]
        [Route("api/v{version:apiVersion}/players/{id}")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpGet]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(PlayerModel))]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task<PlayerModel> GetById([FromRoute][Required] int id)
        {
            try
            {
                return await playerService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "deleteAsync")]
        [Route("api/v{version:apiVersion}/players/delete/{id}")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpDelete]
        [ProducesResponseType((int)HttpResponseType.OK)]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task Delete([Required] [FromRoute] int id)
        {
            try
            {
                await playerService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "CrateAsync")]
        [Route("api/v{version:apiVersion}/players/create")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPost]
        [ProducesResponseType((int)HttpResponseType.OK)]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task CreateAsync([FromBody] [Required] PlayerModel player)
        {
            try
            {
                await playerService.CreateAsync(player);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "updateAsync")]
        [Route("api/v{version:apiVersion}/players/update")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPut]
        [ProducesResponseType((int)HttpResponseType.OK)]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        [Produces("application/json")]
        public async Task UpdateAsync([FromBody][Required] PlayerModel player)
        {
            try
            {
                await playerService.UpdateAsync(player);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
