using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(
        IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetByIdAsync(command.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        var addTrainerResult = gym.AddTrainer(command.TrainerId);

        if (addTrainerResult.IsError)
        {
            return addTrainerResult.Errors;
        }

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Success;
    }
}
