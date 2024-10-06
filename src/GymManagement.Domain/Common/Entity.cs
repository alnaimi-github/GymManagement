namespace GymManagement.Domain.Common;

public abstract class Entity
{
 public Guid Id {get;init;}

 protected readonly List<IDomainEvent> _domainEvents = [];

 public List<IDomainEvent> PopDomainEvents()
 {
    var copy = _domainEvents.ToList();

    _domainEvents.Clear();

    return copy;
 }

 protected Entity(Guid id) => Id = id;

 protected Entity()
 {
 } 

}