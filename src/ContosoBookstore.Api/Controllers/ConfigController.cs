using Microsoft.AspNetCore.Mvc;

namespace ContosoBookstore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;

    public ConfigController(IConfiguration config) => _config = config;

    // Endpoint que el frontend consume para saber qué feature flags están activos.
    // En Fase 4 del integrador esto se conecta a Azure App Configuration.
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            NewBookListing = _config.GetValue<bool>("FeatureFlags:NewBookListing"),
            AppName = _config.GetValue<string>("AppName") ?? "Contoso Bookstore",
            Environment = _config.GetValue<string>("ASPNETCORE_ENVIRONMENT") ?? "Production"
        });
    }
}
