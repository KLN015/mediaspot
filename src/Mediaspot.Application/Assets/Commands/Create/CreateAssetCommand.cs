using MediatR;
using Mediaspot.Domain.Assets;

namespace Mediaspot.Application.Assets.Commands.Create;

public sealed record CreateAssetCommand(
    AssetType Type,
    string ExternalId,
    string Title,
    string? Description,
    string? Language,
    int? Bitrate,
    int? SampleRate,
    int? Channels,
    string? Resolution,
    int? FrameRate,
    string? Codec) : IRequest<Guid>;
