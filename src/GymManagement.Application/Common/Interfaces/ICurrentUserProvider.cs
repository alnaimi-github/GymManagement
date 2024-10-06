using GymManagement.Application.Common.Models;

namespace GymManagement.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}