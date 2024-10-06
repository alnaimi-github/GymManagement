using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using GymManagement.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementContext(
    DbContextOptions options,
     IHttpContextAccessor httpContextAccessor,
    IPublisher publisher) : DbContext(options), IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<Admin> Admins { get; set; } = null!;
   public DbSet<Subscription> Subscriptions { get; set; } = null!;
   public DbSet<Gym> Gyms { get; set; } = null!;
   
   public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public async Task CommitChangesAsync(CancellationToken cancellationToken)
    {
        var domainEvents = DomainEvents();

        await HandleDomainEventsAsync(domainEvents,cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    private List<IDomainEvent> DomainEvents()
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();
        return domainEvents;
    }

    private async Task HandleDomainEventsAsync(List<IDomainEvent> domainEvents,CancellationToken cancellationToken)
    {
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);   
        }
        else
        {
            await PublishDomainEvents(domainEvents,cancellationToken);
        }
    }

    private  async Task PublishDomainEvents(
        List<IDomainEvent> domainEvents,
        CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }

    private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;
    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
         var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
         .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
         ? existingDomainEvents
         :  new Queue<IDomainEvent>();

       domainEvents.ForEach(domainEventsQueue.Enqueue);

     _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
}