using System.Data.Entity.ModelConfiguration;
using VetsEvents.Models;

namespace VetsEvents.Persistence.EntityConfigurations
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(e => e.EventOrganizerId)
                .IsRequired();

            Property(e => e.EventTypeId)
            .IsRequired();

            Property(e => e.Venue)
            .IsRequired()
            .HasMaxLength(255);

            HasMany(e => e.Attendances)
                .WithRequired(a => a.Event)
                .WillCascadeOnDelete(false);

        }
    }
}