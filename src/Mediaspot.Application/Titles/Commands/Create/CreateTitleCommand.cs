using MediatR;
using Mediaspot.Domain.Enums;

namespace Mediaspot.Application.Titles.Commands.Create;

public sealed record CreateTitleCommand(string Name, TitleType Type, string? Description, DateTime? ReleaseDate): IRequest<Guid>;