using GymManagement.Domain.Users;

namespace GymManagement.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);