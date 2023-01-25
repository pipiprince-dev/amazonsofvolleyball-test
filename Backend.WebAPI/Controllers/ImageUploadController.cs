namespace Backend.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ImageUploadController : ControllerBase
    {
        public ImageUploadController()
        {
        }

        [ApiExplorerSettings(GroupName = ControllerDecoration.PublicAPI)]
        [SwaggerOperation(OperationId = "uploadAsync")]
        [Route("api/v{version:apiVersion}/upload/image")]
        [ApiVersion(ControllerDecoration.ActiveVersion)]
        [HttpPost]
        [ProducesResponseType((int)HttpResponseType.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpResponseType.BadRequest)]
        public async Task<string> UploadImageAsync([FromForm] IFormFile file)
        {
            string result = await ImageUploadHandler.SaveFile(file);

            return result;
        }
    }
}
