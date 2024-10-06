using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using Throw;

namespace GymManagement.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(IGymsRepository gymsRepository, ISubscriptionRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(command.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }


        if (!gym.HasRoom(command.RoomId))
        {
            return Error.NotFound(description: "Room not found");
        }

        gym.RemoveRoom(command.RoomId);

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}