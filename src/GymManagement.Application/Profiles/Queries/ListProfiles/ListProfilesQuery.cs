using ErrorOr;
using MediatR;

namespace GymManagement.Application.Profiles.Queries.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;