using ErrorOr;
using GymManagement.Application.Authentication.Common;
using MediatR;

namespace GymManagement.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;