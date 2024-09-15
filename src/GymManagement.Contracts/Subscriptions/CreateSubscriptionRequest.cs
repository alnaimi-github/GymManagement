using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Contracts.Subscriptions;

    public record CreateSubscriptionRequest(
        SubscriptionType SubscriptionType,
        Guid AdminId);
