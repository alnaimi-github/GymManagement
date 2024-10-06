using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Common.Interfaces;

namespace GymManagement.Domain.Users;

public class User : Entity
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }

    private readonly string _passwordHash = null!;

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? adminId = null,
        Guid? participantId = null,
        Guid? trainerId = null,
        Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AdminId = adminId;
        ParticipantId = participantId;
        TrainerId = trainerId;
        _passwordHash = passwordHash;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public ErrorOr<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
        {
            return Error.Conflict(description: "User already has an admin profile");
        }

        AdminId = Guid.NewGuid();

        return AdminId.Value;
    }

    public ErrorOr<Guid> CreateParticipantProfile()
    {
        if (ParticipantId is not null)
        {
            return Error.Conflict(description: "User already has a participant profile");
        }

        ParticipantId = Guid.NewGuid();

        return ParticipantId.Value;
    }

    public ErrorOr<Guid> CreateTrainerProfile()
    {
        if (TrainerId is not null)
        {
            return Error.Conflict(description: "User already has a trainer profile");
        }

        TrainerId = Guid.NewGuid();

        return TrainerId.Value;
    }

    public List<ProfileType> GetProfileTypes()
    {
        List<ProfileType> profileTypes = new();

        if (AdminId is not null)
        {
            profileTypes.Add(ProfileType.Admin);
        }

        if (TrainerId is not null)
        {
            profileTypes.Add(ProfileType.Trainer);
        }

        if (ParticipantId is not null)
        {
            profileTypes.Add(ProfileType.Participant);
        }

        return profileTypes;
    }

    private User() { }
}