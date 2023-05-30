namespace Exponents.API.MultiLevel;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MultiLevelController : ControllerBase
{
    private readonly MultiLevelService service;
    private readonly ILogger<MultiLevelController> logger;

    public MultiLevelController(MultiLevelService service, ILogger<MultiLevelController> logger)
    {
        this.service = service;
        this.logger = logger;
    }

    [HttpGet(Name = "GetMultiLevelExponent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult MultiLevelExponent()
    {
        try
        {
            return this.Ok(this.service.MultiLevelExponentCalculation(4, 5, 6));
        }
        catch (Exception ex)
        {
            var errorMessage = $"MultiLevelExponentCalculation Failed - {ex.Message}";

            this.logger.LogError(ex, ex.Message);

            return this.Problem(
                title: errorMessage,
                detail: ex.StackTrace,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}

