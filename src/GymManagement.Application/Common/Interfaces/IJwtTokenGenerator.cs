using GymManagement.Domain.Users;

namespace GymManagement.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}