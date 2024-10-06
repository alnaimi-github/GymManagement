using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid SubscriptionId)
       : IRequest<ErrorOr<Subscription>>;