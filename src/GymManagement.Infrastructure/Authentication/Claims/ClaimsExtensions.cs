using System.Security.Claims;

namespace GymManagement.Infrastructure.Authentication.Claims;

public static class ClaimsExtensions
{
    public static List<Claim> AddIfValueNotNull(this List<Claim> claims, string type, string? value)
    {
        if (value is not null)
        {
            claims.Add(new Claim(type: type, value: value!));
        }

        return claims;
    }
}