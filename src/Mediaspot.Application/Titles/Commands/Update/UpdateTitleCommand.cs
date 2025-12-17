using MediatR;
using Mediaspot.Domain.Enums;

namespace Mediaspot.Application.Titles.Commands.Update;

public sealed record UpdateTitleCommand(Guid Id, string Name, TitleType Type, string? Description, DateTime? ReleaseDate): IRequest<Guid>;