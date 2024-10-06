using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(
    ISubscriptionRepository subscriptionsRepository,
    IUnitOfWork unitOfWork,
    IAdminsRepository adminsRepository)
        : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionsRepository = subscriptionsRepository;
    private readonly IAdminsRepository _adminsRepository = adminsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }

        var subscription = new Subscription(
            subscriptionType: request.SubscriptionType,
            adminId: request.AdminId);

        if (admin.SubscriptionId is not null)
        {
            return Error.Conflict(description: "Admin already has an active subscription");
        }

        admin.SetSubscription(subscription);

        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        await _adminsRepository.UpdateAsync(admin);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return subscription;
    }
}