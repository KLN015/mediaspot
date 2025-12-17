using Mediaspot.Domain.Common;
using MediatR;

namespace Mediaspot.Domain.Assets.Events;

public sealed record AssetCreated(Guid AssetId) : IDomainEvent, INotification
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
