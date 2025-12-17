using MediatR;

namespace Mediaspot.Application.Titles.Commands.Delete;

public sealed record DeleteTitleCommand(Guid Id) : IRequest<Guid>;