using ErrorOr;
using GymManagement.Application.Authentication.Common;
using MediatR;

namespace GymManagement.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;