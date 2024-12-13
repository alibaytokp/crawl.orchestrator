using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using parser.api.service.Interfaces;
using parser.api.service.Models;

namespace parser.api.service.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdController : ControllerBase
{
    private readonly ILogger<AdController> _logger;
    private readonly IAdProducerService _adProducerService;

    public AdController(IAdProducerService adProducerService, ILogger<AdController> logger)
    {
        _logger = logger;
        _adProducerService = adProducerService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AdMessage adMessage)
    {
        try
        {
            if (adMessage == null || string.IsNullOrEmpty(adMessage.HtmlContent) )
            {
                return BadRequest("Ad content is required.");
            }
            if (string.IsNullOrEmpty(adMessage.ProviderCode))
            {
                return BadRequest("Providercode is required.");
            }

            adMessage.CreatedAt = DateTime.UtcNow;

            await _adProducerService.PublishAdMessageAsync(adMessage);

            return StatusCode(200, new
            {
                Status = true,
                Code = "200",
                Message = "Ad message published successfully."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error queuing ad message for URL: {Url}", adMessage.Url);
            
            return StatusCode(500, new
            {
                Status = false,
                Code = "500",
                Message = $"An error occurred: {ex.Message}"
            });
        }

    }

}
