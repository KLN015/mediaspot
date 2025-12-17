using MediatR;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Commands.Update;
using Mediaspot.Application.Titles.Commands.Delete;
using Mediaspot.Application.Titles.Queries.GetById;
using Mediaspot.Application.Titles.Queries.GetAllPaginated;
using Mediaspot.Application.Common;
using Mediaspot.Domain.Titles;
using Microsoft.AspNetCore.Mvc;
using Mediaspot.Api.DTOs;

namespace Mediaspot.Api.Controllers;

[ApiController]
[Route("api/titles")]
public sealed class TitlesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Title>> GetById(Guid id, CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetTitleByIdQuery(id), ct));
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Title>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        return Ok(await mediator.Send(new GetAllTitlesPaginatedQuery(page, pageSize), ct));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] TitleDto request, CancellationToken ct)
    {
        var command = new CreateTitleCommand(
            request.Name,
            request.Type,
            request.Description,
            request.ReleaseDate
        );

        return Ok(await mediator.Send(command, ct));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] TitleDto request, CancellationToken ct)
    {
        var command = new UpdateTitleCommand(
            id,
            request.Name,
            request.Type,
            request.Description,
            request.ReleaseDate
        );
        
        return Ok(await mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        return Ok(await mediator.Send(new DeleteTitleCommand(id), ct));
    }
}
