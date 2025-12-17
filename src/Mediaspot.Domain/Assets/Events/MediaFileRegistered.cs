using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Assets.Events;

public sealed record MediaFileRegistered(Guid AssetId, Guid MediaFileId) : IDomainEvent, INotification
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
