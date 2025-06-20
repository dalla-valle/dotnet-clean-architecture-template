namespace Microservice_Name_Token.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for handling API errors.
/// </summary>
public class ManagementController : ControllerBase
{
    /// <summary>
    /// Handles errors and returns a problem response.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the problem response.</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error() => this.Problem();

    /// <summary>
    /// Health check endpoint.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the service is healthy.</returns>
    [HttpGet]
    [Route("healthz")]
    public IActionResult Health() => this.Ok("Healthy");

    /// <summary>
    /// Liveness probe endpoint.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the service is alive.</returns>
    [HttpGet]
    [Route("livez")]
    public IActionResult Liveness() => this.Ok("Alive");
}