using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
  {
      builder.HasKey(s => s.Id);

      builder.Property(s => s.Id)
          .ValueGeneratedNever();

      builder.Property("_maxGyms")
          .HasColumnName("MaxGyms");

      builder.Property(s => s.AdminId);

      builder.Property(s => s.SubscriptionType)
          .HasConversion(
              subscriptionType => subscriptionType.Name,
              value => SubscriptionType.FromName(value,false));

      builder.Property<List<Guid>>("_gymIds")
          .HasColumnName("GymIds")
          .HasListOfIdsConverter();
  }
}