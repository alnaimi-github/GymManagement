using GymManagement.Domain.Subscriptions;

namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class Subscriptions
    {
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxSessionsFreeTier = 3;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymsFreeTier = 1;
    }
}