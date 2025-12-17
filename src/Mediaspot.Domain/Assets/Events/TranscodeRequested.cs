using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Assets.Events;

public sealed record TranscodeRequested(Guid AssetId, Guid MediaFileId, string TargetPreset) : IDomainEvent, INotification
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}