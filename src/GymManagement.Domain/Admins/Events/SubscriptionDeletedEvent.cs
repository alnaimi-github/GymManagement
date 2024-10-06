using GymManagement.Domain.Common;

namespace GymManagement.Domain.Admins.Events;

public record SubscriptionDeletedEvent(Guid SubscriptionId) : IDomainEvent;