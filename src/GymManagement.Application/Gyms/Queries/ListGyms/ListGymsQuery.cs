using ErrorOr;
using MediatR;
using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;