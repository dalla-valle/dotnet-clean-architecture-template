namespace Microservice_Name_Token.Api.Controllers;
using Microservice_Name_Token.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microservice_Name_Token.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microservice_Name_Token.Api.Mapper;
using Microservice_Name_Token.Controllers;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Microservice_Name_Token service api.
/// </summary>
[Authorize]
[Route("Microservice_Name_Token")]
public class Microservice_Name_TokenController : ApiController
{
    private readonly IMicroservice_Name_TokenService service;

    /// <summary>
    /// Initializes a new instance of the <see cref="Microservice_Name_TokenController"/> class.
    /// Ctor.
    /// </summary>
    /// <param name="service">Service Microservice_Name_Token.</param>
    public Microservice_Name_TokenController(IMicroservice_Name_TokenService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Post a new Microservice_Name_Token object.
    /// </summary>
    /// <param name="request">Microservice_Name_Token object.</param>
    /// <remarks>
    /// 🔒 Security: Requires the `` OAuth scope.
    /// </remarks>
    /// <returns>A metadata to describe a new Microservice_Name_Token.</returns>
    [HttpPost]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Microservice_Name_TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomProblemDetails))]
    public async Task<IActionResult> PostMicroservice_Name_Token([FromBody] Microservice_Name_TokenRequest request) =>
        (await this.service.CreateNewMicroservice_Name_Token(request))
            .Match(
                response => this.Created(
                    nameof(this.GetMicroservice_Name_Token),
                    response.ToMicroservice_Name_TokenResponse()),
                errors => this.Problem(errors));

    /// <summary>
    /// Get a Microservice_Name_TokenService from id.
    /// </summary>
    /// <param name="id">An Microservice_Name_TokenService id.</param>
    /// <remarks>
    /// 🔒 Security: Requires the `` OAuth scope.
    /// </remarks>
    /// <returns>A Microservice_Name_TokenService object.</returns>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microservice_Name_TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomProblemDetails))]
    public async Task<IActionResult> GetMicroservice_Name_Token(Guid id) =>
        (await this.service.GetMicroservice_Name_Token(id))
            .Match(
                response => this.Ok(response.ToMicroservice_Name_TokenResponse()),
                errors => this.Problem(errors));
}