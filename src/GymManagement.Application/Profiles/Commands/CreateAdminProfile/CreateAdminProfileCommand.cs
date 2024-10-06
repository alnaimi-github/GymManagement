using ErrorOr;
using MediatR;

namespace GymManagement.Application.Profiles.Commands.CreateAdminProfile;

public record CreateAdminProfileCommand(Guid UserId)
    :  IRequest<ErrorOr<Guid>>;
