namespace GymManagement.Application.Services;

public interface ISubscriptionWriteService
{
   Guid CreateSubscription(string subscriptionType,Guid adminId); 
}