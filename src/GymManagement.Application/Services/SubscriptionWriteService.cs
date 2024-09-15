namespace GymManagement.Application.Services;

public class SubscriptionWriteService : ISubscriptionWriteService
{
    public Guid CreateSubscription(string subscriptionType, Guid adminId)
    {
        return  Guid.NewGuid();
    }
}